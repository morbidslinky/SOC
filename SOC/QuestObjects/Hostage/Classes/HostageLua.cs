﻿using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.QuestObjects.Hostage
{
    static class HostageLua
    {
        static readonly LuaFunction InterCall_hostage_pos01 = new LuaFunction("InterCall_hostage_pos01", @"
this.InterCall_hostage_pos01 = function(soldier2GameObjectId, cpID, interName)
  for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do
    if hostageInfo.isTarget then
      TppMarker.EnableQuestTargetMarker(hostageInfo.hostageName)
    else
      TppMarker.Enable(hostageInfo.hostageName,0,""defend"",""map_and_world_only_icon"",0,false,true)
    end
  end
end"); // to come before questCpInterrogation

        static readonly string questCpInterrogation = @"
this.questCpInterrogation = {
  {name = ""enqt1000_271b10"", func = this.InterCall_hostage_pos01,},
}"; // used in OnEnter and OnTerminate as a parameter for SwitchEnableQuestHighIntTable

        static readonly LuaFunction SwitchEnableQuestHighIntTable = new LuaFunction("SwitchEnableQuestHighIntTable", @"
this.SwitchEnableQuestHighIntTable = function(flag, commandPostName, questCpInterrogation)
  local commandPostId = GetGameObjectId(""TppCommandPost2"", commandPostName)
  if flag then
    TppInterrogation.SetQuestHighIntTable(commandPostId, questCpInterrogation)
  else
    TppInterrogation.RemoveQuestHighIntTable(commandPostId, questCpInterrogation)
  end
end"); // called at OnEnter and OnTerminate

        static readonly LuaFunction WarpHostages = new LuaFunction("WarpHostages", @"
function this.WarpHostages()
  for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do
    local gameObjectId= GetGameObjectId(hostageInfo.hostageName)
    if gameObjectId~=GameObject.NULL_ID then
      local position=hostageInfo.position
      local command={id=""Warp"",degRotationY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}
      GameObject.SendCommand(gameObjectId,command)
    end
  end
end");

        static readonly LuaFunction SetHostageAttributes = new LuaFunction("SetHostageAttributes", @"
function this.SetHostageAttributes()
  for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do
    local gameObjectId= GetGameObjectId(hostageInfo.hostageName)
    if gameObjectId~=GameObject.NULL_ID then
	  if hostageInfo.commands then
        for j,hostageCommand in ipairs(hostageInfo.commands)do
	      GameObject.SendCommand(gameObjectId, hostageCommand)
	    end
	  end
    end
  end
end");

        static readonly LuaFunction CheckIsHostage = new LuaFunction("CheckIsHostage", @"
function this.CheckIsHostage(gameId)
  return Tpp.IsHostage(gameId)
end");

        public static void GetDefinition(HostagesDetail hostageDetail, DefinitionLua definitionLua)
        {
            int hostageCount = hostageDetail.hostages.Count;
            BodyInfoEntry hostageBody = NPCBodyInfo.GetBodyInfo(hostageDetail.hostageMetadata.hostageBodyName);

            if (hostageCount > 0)
            {
                definitionLua.AddPackPath("/Assets/tpp/pack/mission2/ih/ih_hostage_base.fpk");
                definitionLua.AddPackPath(hostageBody.missionPackPath);

                definitionLua.AddPackInfo($@"randomFaceListIH = {{ gender = ""{(hostageBody.isFemale ? "FEMALE" : "MALE")}"", count = {hostageCount}}}");
            }
        }

        public static void GetMain(HostagesDetail hostageDetail, MainLua mainLua)
        {
            List<Hostage> hostages = hostageDetail.hostages;
            HostageMetadata meta = hostageDetail.hostageMetadata;

            mainLua.AddToQuestTable(BuildHostageList(hostageDetail));

            if (hostages.Count > 0)
            {
                if (meta.canInterrogate)
                {
                    mainLua.AddToAuxiliary(InterCall_hostage_pos01);
                    mainLua.AddToAuxiliary(questCpInterrogation);
                    mainLua.AddToAuxiliary(SwitchEnableQuestHighIntTable);

                    //mainLua.AddToAuxiliary($"local hostageCount = {hostages.Count}"); unnecessary if MarkerChangeToEnable message isn't a static readonly
                    mainLua.AddToAuxiliary("local hostagei = 0"); // only used for MarkerChangeToEnable's function
                    mainLua.AddToQStep_Main(new QStep_Message("Marker", @"""ChangeToEnable""", $@"function(arg0, arg1)
              if arg0 == StrCode32(""Hostage_0"") then
                hostagei = hostagei + 1
                if hostagei >= {hostages.Count} then
                  this.SwitchEnableQuestHighIntTable(false, CPNAME, this.questCpInterrogation)
                end
              end
            end")); // could be a static readonly message, but a total hostage count would have to be an auxiliary variable
                    
                    mainLua.AddToQStep_Start_OnEnter("this.SwitchEnableQuestHighIntTable(true, CPNAME, this.questCpInterrogation)");
                    mainLua.AddToOnTerminate("this.SwitchEnableQuestHighIntTable(false, CPNAME, this.questCpInterrogation)");
                }

                mainLua.AddToQStep_Main(QStep_MainCommonMessages.genericTargetMessages);
                
                mainLua.AddToQStep_Start_OnEnter(WarpHostages);
                mainLua.AddToAuxiliary(WarpHostages);

                mainLua.AddToQStep_Start_OnEnter(SetHostageAttributes);
                mainLua.AddToAuxiliary(SetHostageAttributes);

                if (hostages.Any(hostage => hostage.isTarget))
                {
                    CheckQuestGenericEnemy hostageCheck = new CheckQuestGenericEnemy(mainLua, CheckIsHostage, meta.objectiveType);
                    foreach (Hostage hostage in hostages)
                    {
                        if (hostage.isTarget)
                            mainLua.AddToTargetList(hostage.GetObjectName());
                    }
                }
            }
        }

        private static Table BuildHostageList(HostagesDetail hostageDetail)
        {
            Table hostageList = new Table("hostageList");
            List<Hostage> hostages = hostageDetail.hostages;
            HostageMetadata meta = hostageDetail.hostageMetadata;

            string scaredCommand = @"{id = ""SetForceScared"",   scared=true, ever=true }";
            string braveCommand = @"{id = ""SetHostage2Flag"",  flag=""disableScared"", on=true }";
            string injuredCommand = @"{id = ""SetHostage2Flag"",  flag=""disableFulton"",on=true }";
            string untiedCommand = @"{id = ""SetHostage2Flag"",  flag=""unlocked"",   on=true,}";

            if (hostages.Count == 0)
                hostageList.Add(@"
        nil ");
            else
                foreach (Hostage hostage in hostages)
                {
                    hostageList.Add($@"
        {{
            hostageName = ""{hostage.GetObjectName()}"",
            isFaceRandom = true,
            isTarget = {hostage.isTarget.ToString().ToLower()},
            voiceType = {{""hostage_a"", ""hostage_b"", {(hostage.language.Equals("english") ? @" ""hostage_c"", ""hostage_d""," : "")}}},
            langType = ""{hostage.language}"", {(hostage.staffType.Equals("NONE") ? "" : $@"
            staffTypeId = TppDefine.STAFF_TYPE_ID.{hostage.staffType},")} {(hostage.skill.Equals("NONE") ? "" : $@"
            skill = ""{hostage.skill}"", ")}
            bodyId = {NPCBodyInfo.GetBodyInfo(meta.hostageBodyName).gameId},
            position = {{pos = {{{hostage.position.coords.xCoord},{hostage.position.coords.yCoord},{hostage.position.coords.zCoord}}}, rotY = {hostage.position.rotation.GetDegreeRotY()},}},
            commands = {{{(hostage.scared.Equals("ALWAYS") ? scaredCommand + "," : (hostage.scared.Equals("NEVER") ? braveCommand + "," : ""))}{(hostage.isInjured ? injuredCommand + "," : "")}{(hostage.isUntied ? untiedCommand + "," : "")}}},
        }}");
                }
            return hostageList;
        }
    }
}
