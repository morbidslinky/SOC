using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Common;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.WalkerGear
{
    static class WalkerLua
    {
        static readonly StrCodeBlock FinishTimerActiveArea = new StrCodeBlock("Timer", "Finish", "OutOfMostActiveArea", new string[] { }, LuaFunction.ToTableEntry("OutOfMostActiveAreaReboundWalkerGear", new string[] { }, @" if inMostActiveQuestArea == false then InfCore.DebugPrint(""Returning Walker Gear to Side Op area...""); this.ReboundWalkerGear(walkerGearGameId); end; "));

        static readonly StrCodeBlock FinishTimerCooldown = new StrCodeBlock("Timer", "Finish", "AnnounceOnceCooldown", new string[] { }, LuaFunction.ToTableEntry("OnAnnounceOnceCooldownSetExitOnce", new string[] { }, " exitOnce = true; "));

        static readonly LuaTableEntry OneTimeAnnounce = LuaFunction.ToTableEntry("OneTimeAnnounce", new string[] { "announceString1", "announceString2", "isFresh" }, " if isFresh == true then InfCore.DebugPrint(announceString1); InfCore.DebugPrint(announceString2); end; return false; ");
        
        static readonly LuaTableEntry ReboundWalkerGear = LuaFunction.ToTableEntry("ReboundWalkerGear", new string[] { "walkerGearGameObjectId" }, @" local commandPos={ id = ""SetPosition"", rotY = playerWGResetPosition.rotY, pos = playerWGResetPosition.pos}; GameObject.SendCommand(walkerGearGameObjectId,commandPos); ");
        
        static readonly LuaTableEntry SetupGearsQuest = LuaFunction.ToTableEntry("SetupGearsQuest", new string[] { "setupOnce" }, " if setupOnce == true then for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"TppCommonWalkerGear2\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then local commandWeapon={ id = \"SetMainWeapon\", weapon = walkerInfo.primaryWeapon}; GameObject.SendCommand(walkerId, commandWeapon); local commandColor = { id = \"SetColoringType\", type = walkerInfo.colorType }; GameObject.SendCommand(walkerId, commandColor); if walkerInfo.riderName then local soldierId = GetGameObjectId( \"TppSoldier2\", walkerInfo.riderName ); local commandRide = { id = \"SetRelativeVehicle\", targetId = walkerId, rideFromBeginning = true  }; GameObject.SendCommand( soldierId, commandRide ); end; local position = walkerInfo.position; local commandPos={ id = \"SetPosition\", rotY = position.rotY, pos = position.pos}; GameObject.SendCommand(walkerId,commandPos); end; end; end; return false; ");
        
        static readonly LuaTableEntry BuildWalkerGameObjectIdList = LuaFunction.ToTableEntry("BuildWalkerGameObjectIdList", new string[] { }, " for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList) do \nlocal walkerId = GetGameObjectId(\"\"TppCommonWalkerGear2\"\",walkerInfo.walkerName); if walkerId ~= GameObject.NULL_ID then questWalkerGearList[walkerId] = walkerInfo.walkerName; end; end; ");

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
                mainLua.qvars.AddOrSet(Lua.TableEntry("questWalkerGearList", new LuaTable()));
                mainLua.qvars.AddOrSet(Lua.TableEntry("playerWGResetPosition"));
                mainLua.qvars.AddOrSet(Lua.TableEntry("walkerGearGameId"));
                mainLua.qvars.AddOrSet(Lua.TableEntry("inMostActiveQuestArea", true));
                mainLua.qvars.AddOrSet(Lua.TableEntry("exitOnce", true));

                mainLua.AddToQuestTable(BuildWalkerList(detail));

                mainLua.qvars.AddOrSet(OneTimeAnnounce);
                mainLua.qvars.AddOrSet(ReboundWalkerGear);

                WalkerGearsVisualizer visualizer = (WalkerGearsVisualizer)detail.GetVisualizer();
                StrCodeBlock ExitTrap = new StrCodeBlock("Trap", "Exit", $"trap_preDeactiveQuestArea_{mainLua.SetupDetails.loadArea}", new string[] { }, LuaFunction.ToTableEntry("OnExitQuestTrapArea", new string[] { }, " inMostActiveQuestArea = false; walkerGearGameId = vars.playerVehicleGameObjectId; if questWalkerGearList[walkerGearGameId] then playerWGResetPosition = {pos= {vars.playerPosX, vars.playerPosY + 1, vars.playerPosZ},rotY= 0,}; GkEventTimerManager.Start(\"\"OutOfMostActiveArea\"\", 7); exitOnce = this.OneTimeAnnounce(\"\"The Walker Gear cannot travel beyond this point.\"\", \"\"Return to the Side Op area.\"\", exitOnce); end; "));
                StrCodeBlock EnterTrap = new StrCodeBlock("Trap", "Enter", $"trap_preDeactiveQuestArea_{mainLua.SetupDetails.loadArea}", new string[] { }, LuaFunction.ToTableEntry("OnEnterQuestTrapArea", new string[] { }, " inMostActiveQuestArea = true; if GkEventTimerManager.IsTimerActive(\"OutOfMostActiveArea\") and walkerGearGameId == vars.playerVehicleGameObjectId then GkEventTimerManager.Stop(\"OutOfMostActiveArea\"); GkEventTimerManager.Start(\"AnnounceOnceCooldown\", 3); end; "));

                mainLua.AddBaseQStep_MainMsgs(ExitTrap, EnterTrap, FinishTimerActiveArea, FinishTimerCooldown);
                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.mechaCaptureTargetMessages);

                mainLua.qvars.AddOrSet(Lua.TableEntry("setupOnce", true));
                mainLua.qvars.AddOrSet(SetupGearsQuest);
                mainLua.OnUpdate.Function.AppendAssignment(
                    Lua.TableIdentifier("qvars", "setupOnce"), 
                    Lua.FunctionCall(Lua.TableIdentifier("qvars", "SetupGearsQuest"), Lua.TableIdentifier("qvars", "setupOnce")));

                mainLua.qvars.AddOrSet(BuildWalkerGameObjectIdList);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"),
                        Lua.TableIdentifier("qvars", "BuildWalkerGameObjectIdList")
                    )
                );

                if (walkers.Any(walker => walker.isTarget))
                {
                    CheckQuestGenericEnemy checkQuestMethod = new CheckQuestGenericEnemy(mainLua, checkWalkerGear, meta.objectiveType);
                    foreach (WalkerGear walker in walkers)
                    {
                        if (walker.isTarget)
                            mainLua.AddToTargetList(walker.GetObjectName());
                    }
                }
            }
        }

        private static Table BuildWalkerList(WalkerGearsDetail walkerDetail)
        {
            Table walkerList = new Table("walkerList");
            List<WalkerGear> walkers = walkerDetail.walkers;
            WalkerGearsMetadata meta = walkerDetail.walkerMetadata;

            foreach (WalkerGear walker in walkers)
            {
                walkerList.Add($@"
        {{
            walkerName = ""{walker.GetObjectName()}"",{(walker.pilot.Equals("NONE") ? "" : $@"
            riderName = ""{walker.pilot}"",")}
            colorType = {GetEnum(walker.paint)},
            primaryWeapon = {GetEnum(walker.weapon)},
            position = {{pos = {{{walker.position.coords.xCoord},{walker.position.coords.yCoord},{walker.position.coords.zCoord}}}, rotY = {walker.position.rotation.GetDegreeRotY()},}},
        }}");
            }
            return walkerList;
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
