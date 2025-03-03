using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
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
        static readonly LuaTableEntry InterCall_hostage_pos01 = LuaFunction.ToTableEntry("InterCall_hostage_pos01", new string[] { "soldier2GameObjectId", "cpID", "interName" }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nif hostageInfo.isTarget then TppMarker.EnableQuestTargetMarker(hostageInfo.hostageName)\nelse TppMarker.Enable(hostageInfo.hostageName,0,\"defend\",\"map_and_world_only_icon\",0,false,true)\nend\nend");
        // to come before questCpInterrogation


        static readonly LuaTableEntry SwitchEnableQuestHighIntTable = LuaFunction.ToTableEntry("SwitchEnableQuestHighIntTable",
            new string[] { "flag", "commandPostName", "questCpInterrogation" },
            " local commandPostId = GetGameObjectId(\"TppCommandPost2\", commandPostName)\nif flag then TppInterrogation.SetQuestHighIntTable(commandPostId, questCpInterrogation)\n else TppInterrogation.RemoveQuestHighIntTable(commandPostId, questCpInterrogation)\n end");

        static readonly LuaTableEntry WarpHostages = LuaFunction.ToTableEntry("WarpHostages", new string[] { }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nlocal gameObjectId= GetGameObjectId(hostageInfo.hostageName)\nif gameObjectId~=GameObject.NULL_ID then local position=hostageInfo.position\nlocal command={id=\"Warp\",degRotationY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}\nGameObject.SendCommand(gameObjectId,command)\nend\nend");
        
        static readonly LuaTableEntry SetHostageAttributes = LuaFunction.ToTableEntry("SetHostageAttributes", new string[] { }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList) do \nlocal gameObjectId= GetGameObjectId(hostageInfo.hostageName)\nif gameObjectId~=GameObject.NULL_ID then if hostageInfo.commands then for j,hostageCommand in ipairs(hostageInfo.commands) do \n GameObject.SendCommand(gameObjectId, hostageCommand)\nend\nend\nend\nend");

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

            mainLua.QUEST_TABLE.AddOrSet(BuildHostageList(hostages, meta));

            if (hostages.Count > 0)
            {
                if (meta.canInterrogate)
                {
                    mainLua.qvars.AddOrSet(InterCall_hostage_pos01);

                    var cpInt = new LuaTable(
                        Lua.TableEntry("name", "enqt1000_271b10"),
                        Lua.TableEntry("func", "this.InterCall_hostage_pos01")
                    );
                    mainLua.qvars.AddOrSet(Lua.TableEntry("questCpInterrogation", cpInt));
                    mainLua.qvars.AddOrSet(SwitchEnableQuestHighIntTable);

                    mainLua.qvars.AddOrSet(Lua.TableEntry("hostagei", 0));// only used for MarkerChangeToEnable's function
                    mainLua.QStep_Main.StrCode32Table.Add(new StrCode32Script(
                        new StrCode32Event("Marker", "ChangeToEnable", "", "arg0", "arg1"),
                        LuaFunction.ToTableEntry(
                            "OnEnableMarkerCheckIntTable",       
                            new string[] { "arg0", "arg1" },
                            $" if arg0 == StrCode32(\"Hostage_0\") then hostagei = hostagei + 1\nif hostagei >= {hostages.Count} then this.SwitchEnableQuestHighIntTable(false, CPNAME, this.questCpInterrogation)\nend\nend")));

                    mainLua.OnAllocate.OnTerminateQuest.AppendLuaValue(Lua.FunctionCall(
                        Lua.TableIdentifier("qvars", "SwitchEnableQuestHighIntTable"),
                            Lua.Boolean(false),
                            Lua.TableIdentifier("qvars", "CPNAME"),
                            Lua.TableIdentifier("qvars", "questCpInterrogation")
                            )
                        );
                }

                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.genericTargetMessages);

                //mainLua.AddToQStep_Start_OnEnter(WarpHostages);
                //mainLua.QvarTable.AddOrSet(WarpHostages);

                mainLua.qvars.AddOrSet(SetHostageAttributes);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"),
                        Lua.TableIdentifier("qvars", "SetHostageAttributes")
                    )
                );

                if (hostages.Any(hostage => hostage.isTarget))
                {
                    CheckQuestGenericEnemy hostageCheck = new CheckQuestGenericEnemy(mainLua, CheckIsHostage, meta.objectiveType);
                    foreach (Hostage hostage in hostages)
                    {
                        if (hostage.isTarget)
                            mainLua.QUEST_TABLE.AddOrSet(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), hostage.GetObjectName()));
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
                    hostageTable.AddOrSet(Lua.TableEntry("skill", hostage.skill));
                }

                if (hostage.staffType != "NONE")
                {
                    hostageTable.AddOrSet(Lua.TableEntry("staffTypeId", Lua.TableIdentifier("TppDefine", "STAFF_TYPE_ID", hostage.staffType)));
                }

                hostageList.AddOrSet(Lua.TableEntry(hostageTable));
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
                voiceTable.AddOrSet(
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
                commandTable.AddOrSet(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("id", "SetForceScared"),
                            Lua.TableEntry("scared", true, false),
                            Lua.TableEntry("ever", true, false)
                        )
                    )
                );
            } else if (hostage.scared == "NEVER") {
                commandTable.AddOrSet(
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
                commandTable.AddOrSet(
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
                commandTable.AddOrSet(
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
    }
}
