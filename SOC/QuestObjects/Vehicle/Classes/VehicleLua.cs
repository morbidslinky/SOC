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
        static readonly LuaFunction WarpVehicles = Create.Function("for i,vehicleInfo in ipairs(this.QUEST_TABLE.vehicleList) do \nlocal gameObjectId= GetGameObjectId(vehicleInfo.locator); if gameObjectId~=GameObject.NULL_ID then local position=vehicleInfo.position; local command={id=\"SetPosition\",rotY=position.rotY,position=Vector3(position.pos[1],position.pos[2],position.pos[3]) } ; GameObject.SendCommand(gameObjectId,command); end; end");
        
        public static void GetMain(VehiclesDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.vehicles.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildVehicleList(detail.vehicles));

                mainLua.QStep_Start.OnEnter.AppendLuaValue(
                    Create.FunctionCall(
                        Create.TableIdentifier("InfCore", "PCall"), WarpVehicles
                    )
                );

                if (detail.vehicles.Any(vehicle => vehicle.isTarget))
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
                            Create.Table(Create.TableEntry(Create.Table(Create.TableEntry("Check", Create.Function("return Tpp.IsVehicle(gameId)", "gameId")), Create.TableEntry("Type", detail.vehicleMetadata.ObjectiveType))))
                        ),
                        methodPair,
                        Create.TableEntry(
                            Create.TableIdentifier("qvars", "CheckQuestMethodPairs"),
                            Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForGenericEnemy"), Create.Variable("qvars.methodPair.TallyGenericTargets"))),
                            true
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    foreach (Vehicle vehicle in detail.vehicles)
                    {
                        if (vehicle.isTarget)
                            mainLua.QUEST_TABLE.Add(Create.TableEntry(Create.TableIdentifier("QUEST_TABLE", "targetList"), Create.Table(Create.TableEntry(vehicle.GetObjectName()))));
                    }
                }
            }
        }

        internal static void GetScriptChoosableValueSets(VehiclesDetail vehiclesDetail, ChoiceKeyValuesList questKeyValues)
        {
            if (vehiclesDetail.vehicles.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Heavy Vehicle Names (Targets)");

                foreach (string gameObjectName in vehiclesDetail.vehicles
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (vehiclesDetail.vehicles.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Heavy Vehicle Names");

                foreach (string gameObjectName in vehiclesDetail.vehicles.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
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
                    vehicleType = Create.TableIdentifier("Vehicle", "type", "EASTERN_WHEELED_ARMORED_VEHICLE"); 
                    subType = Create.TableIdentifier("Vehicle", "subType", "EASTERN_WHEELED_ARMORED_VEHICLE_ROCKET_ARTILLERY");
                }
                else if (VehicleInfo.vehicleLuaName[vehicle.vehicle] == "WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN")
                {
                    vehicleType = Create.TableIdentifier("Vehicle", "type", "WESTERN_WHEELED_ARMORED_VEHICLE");
                    subType = Create.TableIdentifier("Vehicle", "subType", "WESTERN_WHEELED_ARMORED_VEHICLE_TURRET_MACHINE_GUN");
                }
                else
                {
                    vehicleType = Create.TableIdentifier("Vehicle", "type", VehicleInfo.vehicleLuaName[vehicle.vehicle]);
                    subType = null;
                }

                LuaTable vehicleTable = Create.Table(
                    Create.TableEntry("id", "Spawn"),
                    Create.TableEntry("locator", vehicle.GetObjectName()),
                    Create.TableEntry("type", vehicleType),
                    Create.TableEntry("position", 
                        Create.Table(
                            Create.TableEntry("pos", 
                                Create.Table(
                                    Create.TableEntry(vehicle.position.coords.xCoord),
                                    Create.TableEntry(vehicle.position.coords.yCoord),
                                    Create.TableEntry(vehicle.position.coords.zCoord)
                                )
                            ),
                            Create.TableEntry("rotY", vehicle.position.rotation.GetRadianRotY())
                        )
                    )
                );

                if (subType != null)
                {
                    vehicleTable.Add(Create.TableEntry("subType", subType));
                }

                if (vehicle.vehicleClass != "DEFAULT")
                {
                    vehicleTable.Add(Create.TableEntry("class", Create.TableIdentifier("Vehicle", "class", vehicle.vehicleClass)));
                }

                vehicleList.Add(Create.TableEntry(vehicleTable));
            }

            return Create.TableEntry("vehicleList", vehicleList);
        }
    }
}
