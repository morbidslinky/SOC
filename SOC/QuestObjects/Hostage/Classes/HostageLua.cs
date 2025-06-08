using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.GeoTrap;
using SOC.QuestObjects.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOC.QuestObjects.Hostage
{
    static class HostageLua
    {
        static readonly LuaTableEntry SwitchEnableQuestHighIntTable = Create.FunctionAsTableEntry("SwitchEnableQuestHighIntTable",
            new string[] { "flag", "commandPostName", "questCpInterrogation" },
            " local commandPostId = GetGameObjectId(\"TppCommandPost2\", commandPostName)\nif flag then TppInterrogation.SetQuestHighIntTable(commandPostId, questCpInterrogation)\n else TppInterrogation.RemoveQuestHighIntTable(commandPostId, questCpInterrogation)\n end");

        static readonly LuaFunction SetHostageAttributes = Create.Function("for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nlocal gameObjectId= GetGameObjectId(hostageInfo.hostageName)\nif gameObjectId~=GameObject.NULL_ID then if hostageInfo.commands then for j,hostageCommand in ipairs(hostageInfo.commands) do \n GameObject.SendCommand(gameObjectId, hostageCommand)\nend\nend\nend\nend");

        public static void GetDefinition(HostagesDetail hostageDetail, DefinitionScriptBuilder definitionLua)
        {
            int hostageCount = hostageDetail.hostages.Count;
            BodyInfoEntry hostageBody = NPCBodyInfo.GetBodyInfo(hostageDetail.hostageMetadata.hostageBodyName);

            if (hostageCount > 0)
            {
                definitionLua.AddFpkPathToQuestPackList("/Assets/tpp/pack/mission2/ih/ih_hostage_base.fpk");
                definitionLua.AddFpkPathToQuestPackList(hostageBody.missionPackPath);

                definitionLua.SetRandomFaceListIH(hostageBody.isFemale ? "FEMALE" : "MALE", hostageCount);
            }
        }

        public static void GetMain(HostagesDetail hostageDetail, MainScriptBuilder mainLua)
        {
            List<Hostage> hostages = hostageDetail.hostages;
            HostageMetadata meta = hostageDetail.hostageMetadata;

            if (hostages.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildHostageList(hostages, meta));

                if (meta.canInterrogate)
                {
                    var cpInt = new LuaTable(
                        Create.TableEntry("name", "enqt1000_271b10"),
                        Create.TableEntry("func", BuildInterrogationFunction(hostages))
                    );
                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("questCpInterrogation", Create.Table(Create.TableEntry(cpInt))));

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(SwitchEnableQuestHighIntTable);

                    mainLua.QStep_Start.OnEnter.AppendLuaValue(
                        Create.FunctionCall(
                            Create.TableIdentifier("qvars", "SwitchEnableQuestHighIntTable"),
                            Create.Boolean(true),
                            Create.TableIdentifier("qvars", "CPNAME"),
                            Create.TableIdentifier("qvars", "questCpInterrogation")
                        )
                    );

                    mainLua.OnAllocate.OnTerminate.AppendLuaValue(
                        Create.FunctionCall(
                            Create.TableIdentifier("qvars", "SwitchEnableQuestHighIntTable"),
                            Create.Boolean(false),
                            Create.TableIdentifier("qvars", "CPNAME"),
                            Create.TableIdentifier("qvars", "questCpInterrogation")
                        )
                    );
                }

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), SetHostageAttributes
                    )
                );

                if (hostages.Any(hostage => hostage.isTarget))
                {
                    var methodPair = Create.TableEntry("methodPair",
                        Create.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.genericTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Create.TableEntry(
                            Create.TableIdentifier("qvars", "ObjectiveTypeList", "genericTargets"),
                            Create.Table(Create.TableEntry(Create.Table(Create.TableEntry("Check", Create.Function("return Tpp.IsHostage(gameId)", "gameId")), Create.TableEntry("Type", meta.objectiveType))))       
                        ),
                        methodPair,
                        Create.TableEntry(
                            "CheckQuestMethodPairs",
                            Create.Table(
                                Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Create.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Hostage hostage in hostages)
                    {
                        if (hostage.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(hostage.GetObjectName()))));
                    }
                }
            }
        }

        private static LuaTableEntry BuildHostageList(List<Hostage> hostages, HostageMetadata meta)
        {
            LuaTable hostageList = new LuaTable();

            foreach (Hostage hostage in hostages)
            {
                LuaTable hostageTable = Create.Table(
                    Create.TableEntry("hostageName", hostage.GetObjectName()),
                    Create.TableEntry("isFaceRandom", true, false),
                    Create.TableEntry("isTarget", hostage.isTarget, false),
                    Create.TableEntry("voiceType", GetVoiceTable(hostage)),
                    Create.TableEntry("commands", GetCommandTable(hostage)),
                    Create.TableEntry("position",
                        Create.Table(
                            Create.TableEntry("pos",
                                Create.Table(
                                    Create.TableEntry(hostage.position.coords.xCoord),
                                    Create.TableEntry(hostage.position.coords.yCoord),
                                    Create.TableEntry(hostage.position.coords.zCoord)
                                )
                            ),
                            Create.TableEntry("rotY", hostage.position.rotation.GetRadianRotY())
                        )
                    ),
                    Create.TableEntry("langType", hostage.language),
                    Create.TableEntry("bodyId", NPCBodyInfo.GetBodyInfo(meta.hostageBodyName).gameId)
                );

                if (hostage.skill != "NONE")
                {
                    hostageTable.Add(Create.TableEntry("skill", hostage.skill));
                }

                if (hostage.staffType != "NONE")
                {
                    hostageTable.Add(Create.TableEntry("staffTypeId", Create.TableIdentifier("TppDefine", "STAFF_TYPE_ID", hostage.staffType)));
                }

                hostageList.Add(Create.TableEntry(hostageTable));
            }

            return Create.TableEntry("hostageList", hostageList);
        }

        private static LuaTable GetVoiceTable(Hostage hostage)
        {
            LuaTable voiceTable = Create.Table(
                    Create.TableEntry("hostage_a"),
                    Create.TableEntry("hostage_b")
                );

            if (hostage.language.Equals("english"))
            {
                voiceTable.Add(
                    Create.TableEntry("hostage_c"),
                    Create.TableEntry("hostage_d")
                );
            }

            return voiceTable;
        }

        private static LuaTable GetCommandTable(Hostage hostage)
        {
            LuaTable commandTable = new LuaTable();

            if (hostage.scared == "ALWAYS") {
                commandTable.Add(
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("id", "SetForceScared"),
                            Create.TableEntry("scared", true, false),
                            Create.TableEntry("ever", true, false)
                        )
                    )
                );
            } else if (hostage.scared == "NEVER") {
                commandTable.Add(
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("id", "SetHostage2Flag"),
                            Create.TableEntry("flag", "disableScared"),
                            Create.TableEntry("on", true, false)
                        )
                    )
                );
            }

            if (hostage.isInjured)
            {
                commandTable.Add(
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("id", "SetHostage2Flag"),
                            Create.TableEntry("flag", "disableFulton"),
                            Create.TableEntry("on", true, false)
                        )
                    )
                );
            }

            if (hostage.isUntied)
            {
                commandTable.Add(
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("id", "SetHostage2Flag"),
                            Create.TableEntry("flag", "unlocked"),
                            Create.TableEntry("on", true, false)
                        )
                    )
                );
            }

            return commandTable;
        }

        private static LuaFunction BuildInterrogationFunction(List<Hostage> hostages) {
            LuaFunctionBuilder interrogationBuilder = new LuaFunctionBuilder();

            foreach (Hostage hostage in hostages) {
                if (hostage.isTarget)
                {
                    interrogationBuilder.AppendLuaValue(Create.FunctionCall(Create.TableIdentifier("TppMarker", "EnableQuestTargetMarker"), hostage.GetObjectName()));
                }
                else
                {
                    interrogationBuilder.AppendLuaValue(
                        Create.FunctionCall(
                            Create.TableIdentifier("TppMarker", "Enable"), 
                            Create.String(hostage.GetObjectName()), 
                            Create.Number(0), 
                            Create.String("defend"), 
                            Create.String("map_and_world_only_icon"), 
                            Create.Number(0), 
                            Create.Boolean(false), 
                            Create.Boolean(true)
                        )
                    );
                }
            }

            return interrogationBuilder.ToFunction();
        }

        internal static void GetScriptChoosableValueSets(HostagesDetail hostagesDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (hostagesDetail.hostages.Any(hostage => hostage.isTarget))
            {
                ChoiceKeyValues hostageTargetSenders = new ChoiceKeyValues("Prisoner Names (Targets)");

                foreach (string hostageName in hostagesDetail.hostages
                    .Where(hostage => hostage.isTarget)
                    .Select(hostage => hostage.GetObjectName()))
                {
                    hostageTargetSenders.Add(Create.String(hostageName));
                }

                questKeyValues.Add(hostageTargetSenders);
            }

            if (hostagesDetail.hostages.Count > 0)
            {
                ChoiceKeyValues hostageSenders = new ChoiceKeyValues("Prisoner Names");

                foreach (string hostageName in hostagesDetail.hostages.Select(hostage => hostage.GetObjectName()))
                {
                    hostageSenders.Add(Create.String(hostageName));
                }

                questKeyValues.Add(hostageSenders);
            }
        }
    }
}
