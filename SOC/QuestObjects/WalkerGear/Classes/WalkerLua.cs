using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.WalkerGear
{
    static class WalkerLua
    {
        static readonly QStep_Message ExitTrap = new QStep_Message("Trap", @"""Exit""", "questTrapName", @"function()
              inMostActiveQuestArea = false
              walkerGearGameId = vars.playerVehicleGameObjectId
              if questWalkerGearList[walkerGearGameId] then
                playerWGResetPosition = {pos= {vars.playerPosX, vars.playerPosY + 1, vars.playerPosZ},rotY= 0,}
                GkEventTimerManager.Start(""OutOfMostActiveArea"", 7)
                exitOnce = this.OneTimeAnnounce(""The Walker Gear cannot travel beyond this point."", ""Return to the Side Op area."", exitOnce)
              end
            end");

        static readonly QStep_Message EnterTrap = new QStep_Message("Trap", @"""Enter""", "questTrapName", @"function()
              inMostActiveQuestArea = true
              if GkEventTimerManager.IsTimerActive(""OutOfMostActiveArea"") and walkerGearGameId == vars.playerVehicleGameObjectId then
                GkEventTimerManager.Stop(""OutOfMostActiveArea"")
                GkEventTimerManager.Start(""AnnounceOnceCooldown"", 3)
              end
            end");

        static readonly QStep_Message FinishTimerActiveArea = new QStep_Message("Timer", @"""Finish""", @"""OutOfMostActiveArea""", @"function()
              if inMostActiveQuestArea == false then
                InfCore.DebugPrint(""Returning Walker Gear to Side Op area..."")
                this.ReboundWalkerGear(walkerGearGameId)
              end
            end");

        static readonly QStep_Message FinishTimerCooldown = new QStep_Message("Timer", @"""Finish""", @"""AnnounceOnceCooldown""", @"function()
              exitOnce = true
            end");

        static readonly LuaFunction OneTimeAnnounce = new LuaFunction("OneTimeAnnounce", @"
function this.OneTimeAnnounce(announceString1, announceString2, isFresh)
  if isFresh == true then
    InfCore.DebugPrint(announceString1)
    InfCore.DebugPrint(announceString2)
  end

  return false
end");

        static readonly LuaFunction ReboundWalkerGear = new LuaFunction("ReboundWalkerGear", @"
function this.ReboundWalkerGear(walkerGearGameObjectId)
  local commandPos={ id = ""SetPosition"", rotY = playerWGResetPosition.rotY, pos = playerWGResetPosition.pos}
  GameObject.SendCommand(walkerGearGameObjectId,commandPos)
end");

        static readonly LuaFunction SetupGearsQuest = new LuaFunction("SetupGearsQuest", @"
function this.SetupGearsQuest(setupOnce)
  if setupOnce == true then
    for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList)do
      local walkerId = GetGameObjectId(""TppCommonWalkerGear2"",walkerInfo.walkerName)
      if walkerId ~= GameObject.NULL_ID then

        local commandWeapon={ id = ""SetMainWeapon"", weapon = walkerInfo.primaryWeapon}
        GameObject.SendCommand(walkerId, commandWeapon)

        local commandColor = { id = ""SetColoringType"", type = walkerInfo.colorType }
        GameObject.SendCommand(walkerId, commandColor)

        if walkerInfo.riderName then
          local soldierId = GetGameObjectId( ""TppSoldier2"", walkerInfo.riderName )
          local commandRide = { id = ""SetRelativeVehicle"", targetId = walkerId, rideFromBeginning = true  }
          GameObject.SendCommand( soldierId, commandRide )
        end

		local position = walkerInfo.position
        local commandPos={ id = ""SetPosition"", rotY = position.rotY, pos = position.pos}
        GameObject.SendCommand(walkerId,commandPos)
      end
    end
  end
  return false
end");

        static readonly LuaFunction BuildWalkerGameObjectIdList = new LuaFunction("BuildWalkerGameObjectIdList", @"
function this.BuildWalkerGameObjectIdList()
  for i,walkerInfo in ipairs(this.QUEST_TABLE.walkerList)do
    local walkerId = GetGameObjectId(""TppCommonWalkerGear2"",walkerInfo.walkerName)
    if walkerId ~= GameObject.NULL_ID then
      questWalkerGearList[walkerId] = walkerInfo.walkerName
    end
  end
end");

        static readonly LuaFunction checkWalkerGear = new LuaFunction("CheckIsWalkerGear", @"
function this.CheckIsWalkerGear(gameId)
  return Tpp.IsEnemyWalkerGear(gameId)
end");

        internal static void GetDefinition(WalkerGearsDetail walkerDetail, DefinitionLua definitionLua)
        {
            int walkerCount = walkerDetail.walkers.Count;

            if (walkerCount > 0)
            {
                definitionLua.AddPackPath("/Assets/tpp/pack/mission2/common/mis_com_walkergear.fpk");
            }
        }

        internal static void GetMain(WalkerGearsDetail detail, MainLua mainLua)
        {
            List<WalkerGear> walkers = detail.walkers;
            WalkerGearsMetadata meta = detail.walkerMetadata;

            if (detail.walkers.Count > 0)
            {
                mainLua.AddToOpeningVariables("questWalkerGearList", "{}");
                mainLua.AddToOpeningVariables("playerWGResetPosition");
                mainLua.AddToOpeningVariables("walkerGearGameId");
                mainLua.AddToOpeningVariables("inMostActiveQuestArea", "true");
                mainLua.AddToOpeningVariables("exitOnce", "true");

                mainLua.AddToQuestTable(BuildWalkerList(detail));

                mainLua.AddToAuxiliary(OneTimeAnnounce);
                mainLua.AddToAuxiliary(ReboundWalkerGear);
                mainLua.AddToQStep_Main(ExitTrap, EnterTrap, FinishTimerActiveArea, FinishTimerCooldown);
                mainLua.AddToQStep_Main(QStep_MainCommonMessages.mechaCaptureTargetMessages);

                mainLua.AddToAuxiliary("local setupOnce = true");
                mainLua.AddToOnUpdate("setupOnce = this.SetupGearsQuest(setupOnce)");
                mainLua.AddToAuxiliary(SetupGearsQuest);

                mainLua.AddToQStep_Start_OnEnter(BuildWalkerGameObjectIdList);
                mainLua.AddToAuxiliary(BuildWalkerGameObjectIdList);

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
