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
        static readonly LuaTableEntry SwitchEnableQuestHighIntTable = LuaFunction.ToTableEntry("SwitchEnableQuestHighIntTable",
            new string[] { "flag", "commandPostName", "questCpInterrogation" },
            " local commandPostId = GetGameObjectId(\"TppCommandPost2\", commandPostName)\nif flag then TppInterrogation.SetQuestHighIntTable(commandPostId, questCpInterrogation)\n else TppInterrogation.RemoveQuestHighIntTable(commandPostId, questCpInterrogation)\n end");

        static readonly LuaTableEntry WarpHostages = LuaFunction.ToTableEntry("WarpHostages", new string[] { }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nlocal gameObjectId= GetGameObjectId(hostageInfo.hostageName)\nif gameObjectId~=GameObject.NULL_ID then local position=hostageInfo.position\nlocal command={id=\"Warp\",degRotationY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}\nGameObject.SendCommand(gameObjectId,command)\nend\nend");
        
        static readonly LuaFunction SetHostageAttributes = Lua.Function("for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nlocal gameObjectId= GetGameObjectId(hostageInfo.hostageName)\nif gameObjectId~=GameObject.NULL_ID then if hostageInfo.commands then for j,hostageCommand in ipairs(hostageInfo.commands) do \n GameObject.SendCommand(gameObjectId, hostageCommand)\nend\nend\nend\nend");

        static readonly LuaTableEntry CheckIsHostage = LuaFunction.ToTableEntry("CheckIsHostage", new string[] { "gameId" }, " return Tpp.IsHostage(gameId)");

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
                        Lua.TableEntry("name", "enqt1000_271b10"),
                        Lua.TableEntry("func", BuildInterrogationFunction(hostages))
                    );
                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("questCpInterrogation", Lua.Table(Lua.TableEntry(cpInt))));

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(SwitchEnableQuestHighIntTable);

                    mainLua.QStep_Start.OnEnter.AppendLuaValue(
                        Lua.FunctionCall(
                            Lua.TableIdentifier("qvars", "SwitchEnableQuestHighIntTable"),
                            Lua.Boolean(true),
                            Lua.TableIdentifier("qvars", "CPNAME"),
                            Lua.TableIdentifier("qvars", "questCpInterrogation")
                        )
                    );

                    mainLua.OnAllocate.OnTerminate.AppendLuaValue(
                        Lua.FunctionCall(
                            Lua.TableIdentifier("qvars", "SwitchEnableQuestHighIntTable"),
                            Lua.Boolean(false),
                            Lua.TableIdentifier("qvars", "CPNAME"),
                            Lua.TableIdentifier("qvars", "questCpInterrogation")
                        )
                    );
                }

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), SetHostageAttributes
                    )
                );

                if (hostages.Any(hostage => hostage.isTarget))
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        )
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.genericTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Lua.TableEntry(
                            Lua.TableIdentifier("qvars", "ObjectiveTypeList", "genericTargets"),
                            Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return Tpp.IsHostage(gameId)", "gameId")), Lua.TableEntry("Type", meta.objectiveType))))       
                        ),
                        methodPair,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(
                                Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Hostage hostage in hostages)
                    {
                        if (hostage.isTarget)
                            mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(hostage.GetObjectName()))));
                    }
                }
            }
        }

        private static LuaTableEntry BuildHostageList(List<Hostage> hostages, HostageMetadata meta)
        {
            LuaTable hostageList = new LuaTable();

            foreach (Hostage hostage in hostages)
            {
                LuaTable hostageTable = Lua.Table(
                    Lua.TableEntry("hostageName", hostage.GetObjectName()),
                    Lua.TableEntry("isFaceRandom", true, false),
                    Lua.TableEntry("isTarget", hostage.isTarget, false),
                    Lua.TableEntry("voiceType", GetVoiceTable(hostage)),
                    Lua.TableEntry("commands", GetCommandTable(hostage)),
                    Lua.TableEntry("position",
                        Lua.Table(
                            Lua.TableEntry("pos",
                                Lua.Table(
                                    Lua.TableEntry(hostage.position.coords.xCoord),
                                    Lua.TableEntry(hostage.position.coords.yCoord),
                                    Lua.TableEntry(hostage.position.coords.zCoord)
                                )
                            ),
                            Lua.TableEntry("rotY", hostage.position.rotation.GetRadianRotY())
                        )
                    ),
                    Lua.TableEntry("langType", hostage.language),
                    Lua.TableEntry("bodyId", NPCBodyInfo.GetBodyInfo(meta.hostageBodyName).gameId)
                );

                if (hostage.skill != "NONE")
                {
                    hostageTable.Add(Lua.TableEntry("skill", hostage.skill));
                }

                if (hostage.staffType != "NONE")
                {
                    hostageTable.Add(Lua.TableEntry("staffTypeId", Lua.TableIdentifier("TppDefine", "STAFF_TYPE_ID", hostage.staffType)));
                }

                hostageList.Add(Lua.TableEntry(hostageTable));
            }

            return Lua.TableEntry("hostageList", hostageList);
        }

        private static LuaTable GetVoiceTable(Hostage hostage)
        {
            LuaTable voiceTable = Lua.Table(
                    Lua.TableEntry("hostage_a"),
                    Lua.TableEntry("hostage_b")
                );

            if (hostage.language.Equals("english"))
            {
                voiceTable.Add(
                    Lua.TableEntry("hostage_c"),
                    Lua.TableEntry("hostage_d")
                );
            }

            return voiceTable;
        }

        private static LuaTable GetCommandTable(Hostage hostage)
        {
            LuaTable commandTable = new LuaTable();

            if (hostage.scared == "ALWAYS") {
                commandTable.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("id", "SetForceScared"),
                            Lua.TableEntry("scared", true, false),
                            Lua.TableEntry("ever", true, false)
                        )
                    )
                );
            } else if (hostage.scared == "NEVER") {
                commandTable.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("id", "SetHostage2Flag"),
                            Lua.TableEntry("flag", "disableScared"),
                            Lua.TableEntry("on", true, false)
                        )
                    )
                );
            }

            if (hostage.isInjured)
            {
                commandTable.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("id", "SetHostage2Flag"),
                            Lua.TableEntry("flag", "disableFulton"),
                            Lua.TableEntry("on", true, false)
                        )
                    )
                );
            }

            if (hostage.isUntied)
            {
                commandTable.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("id", "SetHostage2Flag"),
                            Lua.TableEntry("flag", "unlocked"),
                            Lua.TableEntry("on", true, false)
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
                    interrogationBuilder.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("TppMarker", "EnableQuestTargetMarker"), hostage.GetObjectName()));
                }
                else
                {
                    interrogationBuilder.AppendLuaValue(
                        Lua.FunctionCall(
                            Lua.TableIdentifier("TppMarker", "Enable"), 
                            Lua.String(hostage.GetObjectName()), 
                            Lua.Number(0), 
                            Lua.String("defend"), 
                            Lua.String("map_and_world_only_icon"), 
                            Lua.Number(0), 
                            Lua.Boolean(false), 
                            Lua.Boolean(true)
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
                ChoiceKeyValues hostageTargetSenders = new ChoiceKeyValues("Prisoners (Targets)");

                foreach (string hostageName in hostagesDetail.hostages
                    .Where(hostage => hostage.isTarget)
                    .Select(hostage => hostage.GetObjectName()))
                {
                    hostageTargetSenders.Add(Lua.FunctionCall("GetGameObjectId", hostageName));
                }

                questKeyValues.Add(hostageTargetSenders);
            }

            if (hostagesDetail.hostages.Count > 0)
            {
                ChoiceKeyValues hostageSenders = new ChoiceKeyValues("Prisoners");

                foreach (string hostageName in hostagesDetail.hostages.Select(hostage => hostage.GetObjectName()))
                {
                    hostageSenders.Add(Lua.FunctionCall("GetGameObjectId", hostageName));
                }

                questKeyValues.Add(hostageSenders);
            }
        }
    }
}
