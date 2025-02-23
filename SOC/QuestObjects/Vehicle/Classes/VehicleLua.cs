using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Vehicle
{
    class VehicleLua
    {
        static readonly LuaTableEntry CheckIsVehicle = LuaFunction.ToTableEntry("CheckIsVehicle", new string[] { "gameId" }, " return Tpp.IsVehicle(gameId) ");

        static readonly LuaTableEntry WarpVehicles = LuaFunction.ToTableEntry("WarpVehicles", new string[] { }, " for i,vehicleInfo in ipairs(this.QUEST_TABLE.vehicleList) do \nlocal gameObjectId= GetGameObjectId(vehicleInfo.locator); if gameObjectId~=GameObject.NULL_ID then local position=vehicleInfo.position; local command={id=\"SetPosition\",rotY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3]) } ; GameObject.SendCommand(gameObjectId,command); end; end");
        
        public static void GetMain(VehiclesDetail detail, MainScriptBuilder mainLua)
        {
            mainLua.AddToQuestTable(BuildVehicleList(detail.vehicles));

            if (detail.vehicles.Count > 0)
            {
                mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.mechaCaptureTargetMessages);

                mainLua.qvars.AddOrSet(WarpVehicles);
                mainLua.QStep_Start.Function.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"),
                        Lua.TableIdentifier("qvars", "WarpVehicles")
                    )
                );

                if (detail.vehicles.Any(vehicle => vehicle.isTarget))
                {
                    CheckQuestGenericEnemy checkQuestMethod = new CheckQuestGenericEnemy(mainLua, CheckIsVehicle, detail.vehicleMetadata.ObjectiveType);
                    foreach (Vehicle vehicle in detail.vehicles)
                    {
                        if (vehicle.isTarget)
                            mainLua.AddToTargetList(vehicle.GetObjectName());
                    }
                }
            }
        }

        private static Table BuildVehicleList(List<Vehicle> vehicles)
        {
            Table vehicleList = new Table("vehicleList");

            if (vehicles.Count == 0)
                vehicleList.Add(@"
        nil");
            else
                foreach (Vehicle vehicle in vehicles)
                {
                    string vehicleType = "NONE"; string subType = "NONE";
                    if (VehicleInfo.vehicleLuaName[vehicle.vehicle] == "EASTERN_WHEELED_ARMORED_VEHICLE_ROCKET_ARTILLERY")
                    {
                        vehicleType = "Vehicle.type.EASTERN_WHEELED_ARMORED_VEHICLE"; subType = "Vehicle.subType.EASTERN_WHEELED_ARMORED_VEHICLE_ROCKET_ARTILLERY";
                    }
                    else if (VehicleInfo.vehicleLuaName[vehicle.vehicle] == "WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN")
                    {
                        vehicleType = "Vehicle.type.WESTERN_WHEELED_ARMORED_VEHICLE"; subType = "Vehicle.subType.WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN";
                    }
                    else
                    {
                        vehicleType = "Vehicle.type." + VehicleInfo.vehicleLuaName[vehicle.vehicle];
                    }
                    vehicleList.Add($@"
        {{
            id = ""Spawn"",
            locator = ""{vehicle.GetObjectName()}"",
            type = {vehicleType}, {(subType == "NONE" ? "" : $@"
            subType = {subType}, ")}{(vehicle.vehicleClass == "DEFAULT" ? "" : $@"
            class = Vehicle.class.{vehicle.vehicleClass}, ")}
            position = {{pos = {{{vehicle.position.coords.xCoord},{vehicle.position.coords.yCoord},{vehicle.position.coords.zCoord}}}, rotY = {vehicle.position.rotation.GetRadianRotY()},}},
        }}");
                }
            return vehicleList;
        }
    }
}
