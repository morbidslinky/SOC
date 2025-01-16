using SOC.Classes.Fox2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Vehicle
{
    class VehicleFox2
    {
        public static void AddQuestEntities(VehiclesDetail details, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Vehicle> vehicles = details.vehicles;
            VehiclesMetadata hMetadata = details.vehicleMetadata;

            if (vehicles.Count() > 0)
            {
                List<Fox2EntityClass> tempVehicleList = new List<Fox2EntityClass>();
                foreach (Vehicle vehicle in vehicles)
                {
                    if (!HasBodyData(entityList, vehicle.vehicle))
                    {
                        entityList.Add(new TppVehicle2BodyData(vehicle.vehicle, dataSet));
                    }
                    if (!HasAttachmentData(entityList, vehicle.vehicle))
                    {
                        entityList.Add(new TppVehicle2AttachmentData(vehicle.vehicle, dataSet));
                    }

                    GameObjectLocator locator = new GameObjectLocator(vehicle.GetObjectName(), dataSet, "TppVehicle2");
                    Transform transform = new Transform(locator, vehicle.position);
                    TppVehicle2LocatorParameter locatorParam = new TppVehicle2LocatorParameter(locator);

                    locator.SetTransform(transform);
                    locator.SetParameter(locatorParam);

                    entityList.Add(locator);
                    entityList.Add(transform);
                    entityList.Add(locatorParam);
                }
            }
        }

        private static bool HasBodyData(List<Fox2EntityClass> entityList, string colloquialName)
        {
            if (VehicleInfo.vehicleBody.ContainsKey(colloquialName))
                return (entityList.Any(entity => entity.GetName() == VehicleInfo.vehicleBody[colloquialName]));

            return true; // technically if it doesn't exist to begin with, the entity list contains the lack of it existing 
        }

        private static bool HasAttachmentData(List<Fox2EntityClass> entityList, string colloquialName)
        {
            if (VehicleInfo.vehicleAttachment.ContainsKey(colloquialName))
                return (entityList.Any(entity => entity.GetName() == VehicleInfo.vehicleAttachment[colloquialName]));

            return true;
        }


    }
}
/*
             foreach (Vehicle vehicle in questDetails.vehicles)
           {
               entityList.Add(new QuestEntity(entityClass.GameObjectLocator, vehicle.name, "TppVehicle2"));
               entityList.Add(new QuestEntity(entityClass.TransformEntity, eDetails: new object[] { vehicle.coordinates, new RotationQuat("0", "0", "0", "1") }));
               entityList.Add(new QuestEntity(entityClass.TppVehicle2LocatorParameter));

               switch (vehicle.vehicleIndex)
               {
                   case 0:
                       if (!vehicleHistory.Contains("veh_bd_east_tnk"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_east_tnk"));
                           vehicleHistory += "veh_bd_east_tnk ";
                       }
                       break;
                   case 1:
                       if (!vehicleHistory.Contains("veh_bd_west_tnk"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_west_tnk"));
                           vehicleHistory += "veh_bd_west_tnk ";
                       }
                       break;
                   case 2:
                       if (!vehicleHistory.Contains("veh_bd_east_wav"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_east_wav"));
                           vehicleHistory += "veh_bd_east_wav ";
                       }
                       break;
                   case 3:
                       if (!vehicleHistory.Contains("veh_bd_east_wav"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_east_wav"));
                           vehicleHistory += "veh_bd_east_wav ";
                       }
                       if (!vehicleHistory.Contains("veh_at_east_wav_rocket"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2AttachmentData, "veh_at_east_wav_rocket"));
                           vehicleHistory += "veh_at_east_wav_rocket ";
                       }
                       break;
                   case 4:
                       if (!vehicleHistory.Contains("veh_bd_west_wav"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_west_wav"));
                           vehicleHistory += "veh_bd_west_wav ";
                       }
                       if (!vehicleHistory.Contains("veh_at_west_wav_trt_machinegun"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2AttachmentData, "veh_at_west_wav_trt_machinegun"));
                           vehicleHistory += "veh_at_west_wav_trt_machinegun ";
                       }
                       break;
                   case 5:
                       if (!vehicleHistory.Contains("veh_bd_west_wav"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2BodyData, "veh_bd_west_wav"));
                           vehicleHistory += "veh_bd_west_wav ";
                       }
                       if (!vehicleHistory.Contains("veh_at_west_wav_trt_cannon"))
                       {
                           entityList.Add(new QuestEntity(entityClass.TppVehicle2AttachmentData, "veh_at_west_wav_trt_cannon"));
                           vehicleHistory += "veh_at_west_wav_trt_cannon ";
                       }
                       break;
               }
*/