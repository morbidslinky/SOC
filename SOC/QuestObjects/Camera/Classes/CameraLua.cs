
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Camera
{
    class CameraLua
    {
        static readonly LuaFunction SetCameraAttributes = Lua.Function("GameObject.SendCommand({{type=\"TppSecurityCamera2\"}}, {{id=\"SetDevelopLevel\", developLevel=6}}); for i,cameraInfo in ipairs(this.QUEST_TABLE.cameraList) do local gameObjectId= GetGameObjectId(cameraInfo.name); if gameObjectId~=GameObject.NULL_ID then if cameraInfo.commands then for j, cameraCommand in ipairs(cameraInfo.commands) do GameObject.SendCommand(gameObjectId, cameraCommand); end; end; end; end; ");
        
        internal static void GetMain(CamerasDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.cameras.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildCameraList(detail.cameras));

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.mechaNoCaptureTargetMessages);

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), SetCameraAttributes
                    )
                );

                if (detail.cameras.Any(camera => camera.isTarget))
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        )
                    );

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        methodPair,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Camera cam in detail.cameras)
                    {
                        if (cam.isTarget)
                            mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(cam.GetObjectName()))));
                    }
                }
            }
        }

        private static LuaTableEntry BuildCameraList(List<Camera> cameras)
        {
            LuaTable cameraList = new LuaTable();

            LuaTable setCPCommand = Lua.Table(
                Lua.TableEntry("id", "SetCommandPost"),
                Lua.TableEntry("cp", Lua.TableIdentifier("qvars","CPNAME"))
            );

            LuaTable enabledCommand = Lua.Table(
                Lua.TableEntry("id", "SetEnabled"),
                Lua.TableEntry("enabled", true, false)
            );

            foreach (Camera camera in cameras)
            {
                LuaTable typeCommand = Lua.Table(
                    Lua.TableEntry("id", camera.weapon ? "SetGunCamera" : "NormalCamera")
                );

                cameraList.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("name", camera.GetObjectName()),
                            Lua.TableEntry("commands", 
                                Lua.Table(
                                    Lua.TableEntry(setCPCommand),
                                    Lua.TableEntry(typeCommand),
                                    Lua.TableEntry(enabledCommand)
                                )
                            )
                        )
                    )
                );
            }

            return Lua.TableEntry("cameraList", cameraList);
        }
    }
}
