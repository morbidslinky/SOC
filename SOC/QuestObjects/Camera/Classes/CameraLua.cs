
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.Camera
{
    class CameraLua
    {
        static readonly LuaFunction SetCameraAttributes = Create.Function("GameObject.SendCommand({{type=\"TppSecurityCamera2\"}}, {{id=\"SetDevelopLevel\", developLevel=6}}); for i,cameraInfo in ipairs(this.QUEST_TABLE.cameraList) do local gameObjectId= GetGameObjectId(cameraInfo.name); if gameObjectId~=GameObject.NULL_ID then if cameraInfo.commands then for j, cameraCommand in ipairs(cameraInfo.commands) do GameObject.SendCommand(gameObjectId, cameraCommand); end; end; end; end; ");
        
        internal static void GetMain(CamerasDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.cameras.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildCameraList(detail.cameras));

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), SetCameraAttributes
                    )
                );

                if (detail.cameras.Any(camera => camera.isTarget))
                {
                    var methodPair = Create.TableEntry("methodPair",
                        Create.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.mechaNoCaptureTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        methodPair,
                        Create.TableEntry(
                            "CheckQuestMethodPairs",
                            Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Create.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Camera cam in detail.cameras)
                    {
                        if (cam.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(cam.GetObjectName()))));
                    }
                }
            }
        }

        internal static void GetScriptChoosableValueSets(CamerasDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.cameras.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Camera Names (Targets)");

                foreach (string gameObjectName in detail.cameras
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.cameras.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Camera Names");

                foreach (string gameObjectName in detail.cameras.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }

        private static LuaTableEntry BuildCameraList(List<Camera> cameras)
        {
            LuaTable cameraList = new LuaTable();

            LuaTable setCPCommand = Create.Table(
                Create.TableEntry("id", "SetCommandPost"),
                Create.TableEntry("cp", Create.TableIdentifier("qvars","CPNAME"))
            );

            LuaTable enabledCommand = Create.Table(
                Create.TableEntry("id", "SetEnabled"),
                Create.TableEntry("enabled", true, false)
            );

            foreach (Camera camera in cameras)
            {
                LuaTable typeCommand = Create.Table(
                    Create.TableEntry("id", camera.weapon ? "SetGunCamera" : "NormalCamera")
                );

                cameraList.Add(
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("name", camera.GetObjectName()),
                            Create.TableEntry("commands", 
                                Create.Table(
                                    Create.TableEntry(setCPCommand),
                                    Create.TableEntry(typeCommand),
                                    Create.TableEntry(enabledCommand)
                                )
                            )
                        )
                    )
                );
            }

            return Create.TableEntry("cameraList", cameraList);
        }
    }
}
