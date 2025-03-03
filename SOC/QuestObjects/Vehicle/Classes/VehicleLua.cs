using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
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
            mainLua.QUEST_TABLE.AddOrSet(BuildVehicleList(detail.vehicles));

            if (detail.vehicles.Count > 0)
            {
                mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.mechaCaptureTargetMessages);

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
                            mainLua.QUEST_TABLE.AddOrSet(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), vehicle.GetObjectName()));
                    }
                }
            }
        }

        private static LuaTableEntry BuildVehicleList(List<Vehicle> vehicles)
        {
            LuaTable vehicleList = new LuaTable();

            foreach (Vehicle vehicle in vehicles)
            {

                LuaTableIdentifier vehicleType;
                LuaTableIdentifier subType;

                if (VehicleInfo.vehicleLuaName[vehicle.vehicle] == "EASTERN_WHEELED_ARMORED_VEHICLE_ROCKET_ARTILLERY")
                {
                    vehicleType = Lua.TableIdentifier("Vehicle", "type", "EASTERN_WHEELED_ARMORED_VEHICLE"); 
                    subType = Lua.TableIdentifier("Vehicle", "subType", "EASTERN_WHEELED_ARMORED_VEHICLE_ROCKET_ARTILLERY");
                }
                else if (VehicleInfo.vehicleLuaName[vehicle.vehicle] == "WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN")
                {
                    vehicleType = Lua.TableIdentifier("Vehicle", "type", "WESTERN_WHEELED_ARMORED_VEHICLE");
                    subType = Lua.TableIdentifier("Vehicle", "subType", "WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN");
                }
                else
                {
                    vehicleType = Lua.TableIdentifier("Vehicle", "type", VehicleInfo.vehicleLuaName[vehicle.vehicle]);
                    subType = null;
                }

                LuaTable vehicleTable = Lua.Table(
                    Lua.TableEntry("id", "Spawn"),
                    Lua.TableEntry("locator", vehicle.GetObjectName()),
                    Lua.TableEntry("type", vehicleType),
                    Lua.TableEntry("position", 
                        Lua.Table(
                            Lua.TableEntry("pos", 
                                Lua.Table(
                                    Lua.TableEntry(vehicle.position.coords.xCoord),
                                    Lua.TableEntry(vehicle.position.coords.yCoord),
                                    Lua.TableEntry(vehicle.position.coords.zCoord)
                                )
                            ),
                            Lua.TableEntry("rotY", vehicle.position.rotation.GetRadianRotY())
                        )
                    )
                );

                if (subType != null)
                {
                    vehicleTable.AddOrSet(Lua.TableEntry("subType", subType));
                }

                if (vehicle.vehicleClass != "DEFAULT")
                {
                    vehicleTable.AddOrSet(Lua.TableEntry("class", Lua.TableIdentifier("Vehicle", "class", vehicle.vehicleClass)));
                }

                vehicleList.AddOrSet(Lua.TableEntry(vehicleTable));
            }

            return Lua.TableEntry("vehicleList", vehicleList);
        }
    }
}
