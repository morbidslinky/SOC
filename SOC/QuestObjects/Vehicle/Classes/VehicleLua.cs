using SOC.Classes.Lua;
using SOC.QuestObjects.Enemy;
using SOC.QuestObjects.WalkerGear;
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

        static readonly LuaFunction WarpVehicles = Lua.Function("for i,vehicleInfo in ipairs(this.QUEST_TABLE.vehicleList) do \nlocal gameObjectId= GetGameObjectId(vehicleInfo.locator); if gameObjectId~=GameObject.NULL_ID then local position=vehicleInfo.position; local command={id=\"SetPosition\",rotY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3]) } ; GameObject.SendCommand(gameObjectId,command); end; end");
        
        public static void GetMain(VehiclesDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.vehicles.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildVehicleList(detail.vehicles));

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Lua.FunctionCall(
                        Lua.TableIdentifier("InfCore", "PCall"), WarpVehicles
                    )
                );

                if (detail.vehicles.Any(vehicle => vehicle.isTarget))
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
                            Lua.Table(Lua.TableEntry(Lua.Table(Lua.TableEntry("Check", Lua.Function("return Tpp.IsVehicle(gameId)", "gameId")), Lua.TableEntry("Type", detail.vehicleMetadata.ObjectiveType))))
                        ),
                        methodPair,
                        Lua.TableEntry(
                            Lua.TableIdentifier("qvars", "CheckQuestMethodPairs"),
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Lua.Variable("qvars.methodPair.TallyGenericTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Vehicle vehicle in detail.vehicles)
                    {
                        if (vehicle.isTarget)
                            mainLua.QUEST_TABLE.Add(Lua.TableEntry(Lua.TableIdentifier("QUEST_TABLE", "targetList"), Lua.Table(Lua.TableEntry(vehicle.GetObjectName()))));
                    }
                }
            }
        }

        internal static void GetScriptChoosableValueSets(VehiclesDetail vehiclesDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (vehiclesDetail.vehicles.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Heavy Vehicles (Targets)");

                foreach (string gameObjectName in vehiclesDetail.vehicles
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (vehiclesDetail.vehicles.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Heavy Vehicles");

                foreach (string gameObjectName in vehiclesDetail.vehicles.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
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
                    vehicleTable.Add(Lua.TableEntry("subType", subType));
                }

                if (vehicle.vehicleClass != "DEFAULT")
                {
                    vehicleTable.Add(Lua.TableEntry("class", Lua.TableIdentifier("Vehicle", "class", vehicle.vehicleClass)));
                }

                vehicleList.Add(Lua.TableEntry(vehicleTable));
            }

            return Lua.TableEntry("vehicleList", vehicleList);
        }
    }
}
