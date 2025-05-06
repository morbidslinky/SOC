using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.Vehicle;

namespace SOC.QuestObjects.WalkerGear
{
    static class WalkerLua
    {
        static readonly Script FinishTimerActiveArea = new Script(
            new StrCode32Event("Timer", "Finish", "OutOfMostActiveArea"), 
            LuaFunction.ToTableEntry("OutOfMostActiveAreaReboundWalkerGear", StrCode32Event.DefaultParameters, @" if qvars.inMostActiveQuestArea == false then InfCore.DebugPrint(""Returning Walker Gear to Side Op area...""); qvars.ReboundWalkerGear(qvars.walkerGearGameId); end; "));

        static readonly Script FinishTimerCooldown = new Script(
            new StrCode32Event("Timer", "Finish", "AnnounceOnceCooldown"), 
            LuaFunction.ToTableEntry("OnAnnounceOnceCooldownSetExitOnce", StrCode32Event.DefaultParameters, " qvars.exitOnce = true; "));

        static readonly LuaTableEntry OneTimeAnnounce = LuaFunction.ToTableEntry("OneTimeAnnounce", new string[] { "announceString1", "announceString2", "isFresh" }, " if isFresh == true then InfCore.DebugPrint(announceString1); InfCore.DebugPrint(announceString2); end; return false; ");
        
        static readonly LuaTableEntry ReboundWalkerGear = LuaFunction.ToTableEntry("ReboundWalkerGear", new string[] { "walkerGearGameObjectId" }, @" local commandPos={ id = ""SetPosition"", rotY = qvars.playerWGResetPosition.rotY, pos = qvars.playerWGResetPosition.pos}; GameObject.SendCommand(walkerGearGameObjectId,commandPos); ");
        
        static readonly LuaFunction SetupGearsQuest = Lua.Function("for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"TppCommonWalkerGear2\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then local commandWeapon={ id = \"SetMainWeapon\", weapon = walkerInfo.primaryWeapon}; GameObject.SendCommand(walkerId, commandWeapon); local commandColor = { id = \"SetColoringType\", type = walkerInfo.colorType }; GameObject.SendCommand(walkerId, commandColor); if walkerInfo.riderName then local soldierId = GetGameObjectId( \"TppSoldier2\", walkerInfo.riderName ); local commandRide = { id = \"SetRelativeVehicle\", targetId = walkerId, rideFromBeginning = true  }; GameObject.SendCommand( soldierId, commandRide ); end; local position = walkerInfo.position; local commandPos={ id = \"SetPosition\", rotY = position.rotY, pos = position.pos}; GameObject.SendCommand(walkerId,commandPos); end; end;");
        
        static readonly LuaFunction BuildWalkerGameObjectIdList = Lua.Function("for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"TppCommonWalkerGear2\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then qvars.questWalkerGearList[walkerId] = walkerInfo.walkerName; end; end; ");

        static readonly LuaTableEntry checkWalkerGear = LuaFunction.ToTableEntry("CheckIsWalkerGear", new string[] { "gameId" }, " return Tpp.IsEnemyWalkerGear(gameId); ");

        internal static void GetDefinition(WalkerGearsDetail walkerDetail, DefinitionScriptBuilder definitionLua)
        {
            int walkerCount = walkerDetail.walkers.Count;

            if (walkerCount > 0)
            {
                definitionLua.AddFpkPathToQuestPackList("/Assets/tpp/pack/mission2/common/mis_com_walkergear.fpk");
            }
        }

        internal static void GetMain(WalkerGearsDetail detail, MainScriptBuilder mainLua)
        {
            List<WalkerGear> walkers = detail.walkers;
            WalkerGearsMetadata meta = detail.walkerMetadata;

            if (detail.walkers.Count > 0)
            {
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("questWalkerGearList", new LuaTable()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("playerWGResetPosition", new LuaTable()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("walkerGearGameId", Lua.Nil()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("inMostActiveQuestArea", true, false));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Lua.TableEntry("exitOnce", true, false));

                mainLua.QUEST_TABLE.Add(BuildWalkerList(walkers));

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(OneTimeAnnounce);
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(ReboundWalkerGear);

                WalkerGearsControlPanel controlPanel = (WalkerGearsControlPanel)detail.GetControlPanel();
                Script ExitTrap = new Script(
                    new StrCode32Event("Trap", "Exit", $"trap_preDeactiveQuestArea_{mainLua.Quest.SetupDetails.loadArea}"),
                    LuaFunction.ToTableEntry("OnExitQuestTrapArea", StrCode32Event.DefaultParameters, " qvars.inMostActiveQuestArea = false; qvars.walkerGearGameId = vars.playerVehicleGameObjectId; if qvars.questWalkerGearList[qvars.walkerGearGameId] then qvars.playerWGResetPosition = {pos= {vars.playerPosX, vars.playerPosY + 1, vars.playerPosZ},rotY= 0,}; GkEventTimerManager.Start(\"OutOfMostActiveArea\", 7); qvars.exitOnce = qvars.OneTimeAnnounce(\"The Walker Gear cannot travel beyond this point.\", \"Return to the Side Op area.\", qvars.exitOnce); end; "));
                Script EnterTrap = new Script(
                    new StrCode32Event("Trap", "Enter", $"trap_preDeactiveQuestArea_{mainLua.Quest.SetupDetails.loadArea}"), 
                    LuaFunction.ToTableEntry("OnEnterQuestTrapArea", StrCode32Event.DefaultParameters, " qvars.inMostActiveQuestArea = true; if GkEventTimerManager.IsTimerActive(\"OutOfMostActiveArea\") and qvars.walkerGearGameId == vars.playerVehicleGameObjectId then GkEventTimerManager.Stop(\"OutOfMostActiveArea\"); GkEventTimerManager.Start(\"AnnounceOnceCooldown\", 3); end; "));

                mainLua.QStep_Main.StrCode32Table.Add(ExitTrap, EnterTrap, FinishTimerActiveArea, FinishTimerCooldown);

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), BuildWalkerGameObjectIdList
                    )
                );

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), SetupGearsQuest
                    )
                );

                if (walkers.Any(walker => walker.isTarget))
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        )
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.mechaCaptureTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Lua.TableEntry(
                            Lua.TableIdentifier("qvars", "ObjectiveTypeList", "genericTargets"),
                            Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return Tpp.IsEnemyWalkerGear(gameId)", "gameId")), Lua.TableEntry("Type", meta.objectiveType))))
                        ),
                        methodPair,
                        Lua.TableEntry(
                            Lua.TableIdentifier("qvars", "CheckQuestMethodPairs"),
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (WalkerGear walker in walkers)
                    {
                        if (walker.isTarget)
                            mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(walker.GetObjectName()))));
                    }
                }
            }
        }

        private static LuaTableEntry BuildWalkerList(List<WalkerGear> walkers)
        {
            LuaTable walkerList = new LuaTable();

            foreach (WalkerGear walker in walkers)
            {
                LuaTable walkerTable = Lua.Table(
                    Lua.TableEntry("walkerName", walker.GetObjectName()),
                    Lua.TableEntry("colorType", GetEnum(walker.paint)),
                    Lua.TableEntry("primaryWeapon", GetEnum(walker.weapon)),
                    Lua.TableEntry("position",
                        Lua.Table(
                            Lua.TableEntry("pos",
                                Lua.Table(
                                    Lua.TableEntry(walker.position.coords.xCoord),
                                    Lua.TableEntry(walker.position.coords.yCoord),
                                    Lua.TableEntry(walker.position.coords.zCoord)
                                )
                            ),
                            Lua.TableEntry("rotY", walker.position.rotation.GetRadianRotY())
                        )
                    )
                );

                if (walker.pilot != "NONE")
                {
                    walkerTable.Add(Lua.TableEntry("riderName", walker.pilot));
                }

                walkerList.Add(Lua.TableEntry(walkerTable));
            }

            return Lua.TableEntry("walkerList", walkerList);
        }

        private static int GetEnum(string value)
        {
            switch (value)
            {
                case "SOVIET":
                    return (int)color.SOVIET;
                case "ROGUE_COYOTE":
                    return (int)color.ROGUE_COYOTE;
                case "CFA":
                    return (int)color.CFA;
                case "ZRS":
                    return (int)color.ZRS;
                case "DDOGS":
                    return (int)color.DDOGS;
                case "WG_MACHINEGUN":
                    return (int)weapon.WG_MACHINEGUN;
                case "WG_MISSILE":
                    return (int)weapon.WG_MISSILE;
                default:
                    return -1;
            }
        }

        private enum color { SOVIET, ROGUE_COYOTE, CFA, ZRS, DDOGS }
        private enum weapon { WG_MACHINEGUN, WG_MISSILE }
    }
}
