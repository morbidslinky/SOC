﻿using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Vehicle
{
    class VehicleLua
    {
        public static void GetMain(VehicleDetail detail, MainLua mainLua)
        {
            mainLua.AddToLocalVariables("local vehicleQuestType =", "local vehicleQuestType = " + detail.vehicleMetadata.ObjectiveType);
            mainLua.AddToQuestTable(BuildVehicleList(detail.vehicles));

            foreach (Vehicle vehicle in detail.vehicles)
            {
                if (vehicle.isTarget)
                    mainLua.AddToTargetList(vehicle.GetObjectName());
            }
        }

        private static string BuildVehicleList(List<Vehicle> vehicles)
        {
            StringBuilder vehicleListBuilder = new StringBuilder("vehicleList = {");

            if (vehicles.Count == 0)
                vehicleListBuilder.Append(@"
        nil ");
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
                        vehicleType = VehicleInfo.vehicleLuaName[vehicle.vehicle];
                    }
                    vehicleListBuilder.Append($@"
        {{
            id = ""Spawn"",
            locator = ""{vehicle.GetObjectName()}"",
            type = {vehicleType},
            {(subType == "NONE" ? "" : $@"subType = {subType}, ")}
            {(vehicle.vehicleClass == "DEFAULT" ? "" : $@"class = Vehicle.class.{vehicle.vehicleClass}, ")}
            position = {{pos = {{{vehicle.position.coords.xCoord},{vehicle.position.coords.yCoord},{vehicle.position.coords.zCoord}}}, rotY = {vehicle.position.rotation.GetRadianRotY()},}},");
                    vehicleListBuilder.Append(@"
        },");
                }
            vehicleListBuilder.Append(@"
    }");
            return vehicleListBuilder.ToString();
        }
    }
}