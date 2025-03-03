
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
        static readonly LuaTableEntry SetCameraAttributes = LuaFunction.ToTableEntry(
            "SetCameraAttributes", 
            new string[] {},
            "GameObject.SendCommand({{type=\"TppSecurityCamera2\"}}, {{id=\"SetDevelopLevel\", developLevel=6}}); for i,cameraInfo in ipairs(this.QUEST_TABLE.cameraList) do local gameObjectId= GetGameObjectId(cameraInfo.name); if gameObjectId~=GameObject.NULL_ID then if cameraInfo.commands then for j, cameraCommand in ipairs(cameraInfo.commands) do GameObject.SendCommand(gameObjectId, cameraCommand); end; end; end; end; ");
        
        internal static void GetMain(CamerasDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.cameras.Count > 0)
            {
                mainLua.QUEST_TABLE.AddOrSet(BuildCameraList(detail.cameras));

                mainLua.QStep_Main.StrCode32Table.Add(QStep_MainCommonMessages.mechaNoCaptureTargetMessages);

                mainLua.qvars.AddOrSet(SetCameraAttributes);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), 
                        Lua.TableIdentifier("qvars", "SetCameraAttributes")
                    )
                );

                if (detail.cameras.Any(camera => camera.isTarget))
                {
                    CheckQuestGenericEnemy cameraCheck = new CheckQuestGenericEnemy(mainLua);
                    foreach (Camera cam in detail.cameras)
                    {
                        if (cam.isTarget)
                            mainLua.QUEST_TABLE.AddOrSet(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), cam.GetObjectName()));
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

                cameraList.AddOrSet(
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
