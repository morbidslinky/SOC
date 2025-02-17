using SOC.Classes.Common;
using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.QuestObjects.Hostage
{
    static class HostageLua
    {
        static readonly LuaFunctionOldFormat InterCall_hostage_pos01 = new LuaFunctionOldFormat("InterCall_hostage_pos01", new string[] { "soldier2GameObjectId", "cpID", "interName" }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do if hostageInfo.isTarget then TppMarker.EnableQuestTargetMarker(hostageInfo.hostageName); else TppMarker.Enable(hostageInfo.hostageName,0,\"defend\",\"map_and_world_only_icon\",0,false,true); end; end; ");
        // to come before questCpInterrogation

        static readonly string questCpInterrogation = @" this.questCpInterrogation = {{name = ""enqt1000_271b10"", func = this.InterCall_hostage_pos01,}} ";

        static readonly LuaFunctionOldFormat SwitchEnableQuestHighIntTable = new LuaFunctionOldFormat("SwitchEnableQuestHighIntTable",
            new string[] { "flag", "commandPostName", "questCpInterrogation" },
            " local commandPostId = GetGameObjectId(\"TppCommandPost2\", commandPostName); if flag then TppInterrogation.SetQuestHighIntTable(commandPostId, questCpInterrogation); else TppInterrogation.RemoveQuestHighIntTable(commandPostId, questCpInterrogation); end; ");

        static readonly LuaFunctionOldFormat WarpHostages = new LuaFunctionOldFormat("WarpHostages", new string[] { }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do local gameObjectId= GetGameObjectId(hostageInfo.hostageName); if gameObjectId~=GameObject.NULL_ID then local position=hostageInfo.position; local command={id=\"Warp\",degRotationY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3])}; GameObject.SendCommand(gameObjectId,command); end; end; ");
        
        static readonly LuaFunctionOldFormat SetHostageAttributes = new LuaFunctionOldFormat("SetHostageAttributes", new string[] { }, " for i,hostageInfo in ipairs(this.QUEST_TABLE.hostageList)do local gameObjectId= GetGameObjectId(hostageInfo.hostageName); if gameObjectId~=GameObject.NULL_ID then if hostageInfo.commands then for j,hostageCommand in ipairs(hostageInfo.commands)do GameObject.SendCommand(gameObjectId, hostageCommand); end; end; end; end; ");

        static readonly LuaFunctionOldFormat CheckIsHostage = new LuaFunctionOldFormat("CheckIsHostage", new string[] { "gameId" }, " return Tpp.IsHostage(gameId); ");

        public static void GetDefinition(HostagesDetail hostageDetail, DefinitionLuaBuilder definitionLua)
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

        public static void GetMain(HostagesDetail hostageDetail, MainLuaBuilder mainLua)
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

                    mainLua.AddToAuxiliary("local hostagei = 0"); // only used for MarkerChangeToEnable's function
                    mainLua.AddBaseQStep_MainMsgs(new StrCodeBlock(
                        "Marker", 
                        "ChangeToEnable", 
                        new string[] { "arg0", "arg1" },
                        new LuaFunctionOldFormat(
                            "OnEnableMarkerCheckIntTable",       
                            new string[] { "arg0", "arg1" },
                            $@" if arg0 == StrCode32(""Hostage_0"") then hostagei = hostagei + 1; if hostagei >= {hostages.Count} then this.SwitchEnableQuestHighIntTable(false, CPNAME, this.questCpInterrogation); end; end; ")));
                    
                    mainLua.AddToQStep_Start_OnEnter("this.SwitchEnableQuestHighIntTable(true, CPNAME, this.questCpInterrogation)");
                    mainLua.AddToOnTerminate("this.SwitchEnableQuestHighIntTable(false, CPNAME, this.questCpInterrogation)");
                }

                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.genericTargetMessages);
                
                //mainLua.AddToQStep_Start_OnEnter(WarpHostages);
                //mainLua.AddToAuxiliary(WarpHostages);

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
