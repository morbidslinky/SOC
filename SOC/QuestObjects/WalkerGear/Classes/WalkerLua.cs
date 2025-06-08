using SOC.Classes.Common;
using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.Hostage;
using SOC.QuestObjects.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.WalkerGear
{
    static class WalkerLua
    {
        static readonly Script FinishTimerActiveArea = new Script(
            new StrCode32("Timer", Create.String("Finish"), "", Create.String("OutOfMostActiveArea")), 
            Create.FunctionAsTableEntry("OutOfMostActiveAreaReboundWalkerGear", StrCode32.DefaultParameters, @" if qvars.inMostActiveQuestArea == false then InfCore.DebugPrint(""Returning Walker Gear to Side Op area...""); qvars.ReboundWalkerGear(qvars.walkerGearGameId); end; ", true));

        static readonly Script FinishTimerCooldown = new Script(
            new StrCode32("Timer", Create.String("Finish"), "", Create.String("AnnounceOnceCooldown")), 
            Create.FunctionAsTableEntry("OnAnnounceOnceCooldownSetExitOnce", StrCode32.DefaultParameters, " qvars.exitOnce = true; ", true));

        static readonly LuaTableEntry OneTimeAnnounce = Create.FunctionAsTableEntry("OneTimeAnnounce", new string[] { "announceString1", "announceString2", "isFresh" }, " if isFresh == true then InfCore.DebugPrint(announceString1); InfCore.DebugPrint(announceString2); end; return false; ");
        
        static readonly LuaTableEntry ReboundWalkerGear = Create.FunctionAsTableEntry("ReboundWalkerGear", new string[] { "walkerGearGameObjectId" }, @" local commandPos={ id = ""SetPosition"", rotY = qvars.playerWGResetPosition.rotY, pos = qvars.playerWGResetPosition.pos}; GameObject.SendCommand(walkerGearGameObjectId,commandPos); ", true);
        
        static readonly LuaFunction SetupGearsQuest = Create.Function("for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"TppCommonWalkerGear2\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then local commandWeapon={ id = \"SetMainWeapon\", weapon = walkerInfo.primaryWeapon}; GameObject.SendCommand(walkerId, commandWeapon); local commandColor = { id = \"SetColoringType\", type = walkerInfo.colorType }; GameObject.SendCommand(walkerId, commandColor); if walkerInfo.riderName then local soldierId = GetGameObjectId( \"TppSoldier2\", walkerInfo.riderName ); local commandRide = { id = \"SetRelativeVehicle\", targetId = walkerId, rideFromBeginning = true  }; GameObject.SendCommand( soldierId, commandRide ); end; local position = walkerInfo.position; local commandPos={ id = \"SetPosition\", rotY = position.rotY, pos = position.pos}; GameObject.SendCommand(walkerId,commandPos); end; end;");
        
        static readonly LuaFunction BuildWalkerGameObjectIdList = Create.Function("for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"TppCommonWalkerGear2\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then qvars.questWalkerGearList[walkerId] = walkerInfo.walkerName; end; end; ");

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
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("questWalkerGearList", new LuaTable()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("playerWGResetPosition", new LuaTable()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("walkerGearGameId", Create.Nil()));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("inMostActiveQuestArea", true, false));
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(Create.TableEntry("exitOnce", true, false));

                mainLua.QUEST_TABLE.Add(BuildWalkerList(walkers));

                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(OneTimeAnnounce);
                mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(ReboundWalkerGear);

                WalkerGearsControlPanel controlPanel = (WalkerGearsControlPanel)detail.GetControlPanel();
                Script ExitTrap = new Script(
                    new StrCode32("Trap", Create.String("Exit"), "", Create.String($"trap_preDeactiveQuestArea_{mainLua.Quest.SetupDetails.loadArea}")),
                    Create.FunctionAsTableEntry("OnExitQuestTrapArea", StrCode32.DefaultParameters, " qvars.inMostActiveQuestArea = false; qvars.walkerGearGameId = vars.playerVehicleGameObjectId; if qvars.questWalkerGearList[qvars.walkerGearGameId] then qvars.playerWGResetPosition = {pos= {vars.playerPosX, vars.playerPosY + 1, vars.playerPosZ},rotY= 0,}; GkEventTimerManager.Start(\"OutOfMostActiveArea\", 7); qvars.exitOnce = qvars.OneTimeAnnounce(\"The Walker Gear cannot travel beyond this point.\", \"Return to the Side Op area.\", qvars.exitOnce); end; ", true));
                Script EnterTrap = new Script(
                    new StrCode32("Trap", Create.String("Enter"), "", Create.String($"trap_preDeactiveQuestArea_{mainLua.Quest.SetupDetails.loadArea}")), 
                    Create.FunctionAsTableEntry("OnEnterQuestTrapArea", StrCode32.DefaultParameters, " qvars.inMostActiveQuestArea = true; if GkEventTimerManager.IsTimerActive(\"OutOfMostActiveArea\") and qvars.walkerGearGameId == vars.playerVehicleGameObjectId then GkEventTimerManager.Stop(\"OutOfMostActiveArea\"); GkEventTimerManager.Start(\"AnnounceOnceCooldown\", 3); end; ", true));

                mainLua.QStep_Main.StrCode32Table.Add(ExitTrap, EnterTrap, FinishTimerActiveArea, FinishTimerCooldown);

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), BuildWalkerGameObjectIdList
                    )
                );

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), SetupGearsQuest
                    )
                );

                if (walkers.Any(walker => walker.isTarget))
                {
                    var methodPair = Create.TableEntry("methodPair",
                        Create.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForGenericEnemy,
                            StaticObjectiveFunctions.TallyGenericTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.mechaCaptureTargetMessages);

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Create.TableEntry(
                            Create.TableIdentifier("qvars", "ObjectiveTypeList", "genericTargets"),
                            Create.Table(Create.TableEntry(Create.Table(Create.TableEntry("Check", Create.Function("return Tpp.IsEnemyWalkerGear(gameId)", "gameId")), Create.TableEntry("Type", meta.objectiveType))))
                        ),
                        methodPair,
                        Create.TableEntry(
                            Create.TableIdentifier("qvars", "CheckQuestMethodPairs"),
                            Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Create.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (WalkerGear walker in walkers)
                    {
                        if (walker.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(walker.GetObjectName()))));
                    }
                }
            }
        }

        internal static void GetScriptChoosableValueSets(WalkerGearsDetail walkerGearsDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (walkerGearsDetail.walkers.Any(walker => walker.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Walker Gear Names (Targets)");

                foreach (string gameObjectName in walkerGearsDetail.walkers
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (walkerGearsDetail.walkers.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Walker Gear Names");

                foreach (string gameObjectName in walkerGearsDetail.walkers.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
        }

        private static LuaTableEntry BuildWalkerList(List<WalkerGear> walkers)
        {
            LuaTable walkerList = new LuaTable();

            foreach (WalkerGear walker in walkers)
            {
                LuaTable walkerTable = Create.Table(
                    Create.TableEntry("walkerName", walker.GetObjectName()),
                    Create.TableEntry("colorType", GetEnum(walker.paint)),
                    Create.TableEntry("primaryWeapon", GetEnum(walker.weapon)),
                    Create.TableEntry("position",
                        Create.Table(
                            Create.TableEntry("pos",
                                Create.Table(
                                    Create.TableEntry(walker.position.coords.xCoord),
                                    Create.TableEntry(walker.position.coords.yCoord),
                                    Create.TableEntry(walker.position.coords.zCoord)
                                )
                            ),
                            Create.TableEntry("rotY", walker.position.rotation.GetRadianRotY())
                        )
                    )
                );

                if (walker.pilot != "NONE")
                {
                    walkerTable.Add(Create.TableEntry("riderName", walker.pilot));
                }

                walkerList.Add(Create.TableEntry(walkerTable));
            }

            return Create.TableEntry("walkerList", walkerList);
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
