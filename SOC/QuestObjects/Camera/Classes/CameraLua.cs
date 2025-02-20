
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Lua;

namespace SOC.QuestObjects.Camera
{
    class CameraLua
    {
        static readonly LuaTableEntry SetCameraAttributes = LuaFunction.ToTableEntry(
            "SetCameraAttributes", 
            new string[] {},
            "GameObject.SendCommand({{type=\"TppSecurityCamera2\"}}, {{id=\"SetDevelopLevel\", developLevel=6}}); for i,cameraInfo in ipairs(this.QUEST_TABLE.cameraList) do local gameObjectId= GetGameObjectId(cameraInfo.name); if gameObjectId~=GameObject.NULL_ID then if cameraInfo.commands then for j, cameraCommand in ipairs(cameraInfo.commands) do GameObject.SendCommand(gameObjectId, cameraCommand); end; end; end; end; ");
        
        internal static void GetMain(CamerasDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.cameras.Count > 0)
            {
                mainLua.AddToQuestTable(BuildCameraList(detail.cameras));

                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.mechaNoCaptureTargetMessages);

                mainLua.AddToQStep_Start_OnEnter(SetCameraAttributes);
                mainLua.AddToQuestVariablesTable(SetCameraAttributes);

                if(detail.cameras.Any(camera => camera.isTarget))
                {
                    CheckQuestGenericEnemy cameraCheck = new CheckQuestGenericEnemy(mainLua);
                    foreach (Camera cam in detail.cameras)
                    {
                        if (cam.isTarget)
                            mainLua.AddToTargetList(cam.GetObjectName());
                    }
                }
            }
        }

        private static Table BuildCameraList(List<Camera> cameras)
        {
            Table cameraList = new Table("cameraList");
            string setCPCommand = @"{id = ""SetCommandPost"", cp=CPNAME}";
            string typeCommand = @"{id=""NormalCamera""}";
            string enabledCommand = @"{id=""SetEnabled"", enabled=true}";

            foreach (Camera camera in cameras)
            {
                typeCommand = $@"{{id=""{(camera.weapon ? "SetGunCamera" : "NormalCamera")}""}} ";
                
                cameraList.Add($@"
        {{
            name = ""{camera.GetObjectName()}"",
            commands = {{{setCPCommand}, {typeCommand}, {enabledCommand}}},
        }}");
            }

            return cameraList;
        }
    }
}
