using SOC.Classes.Common;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SOC.QuestObjects.Common
{
    public class ObjectsDetails
    {
        [XmlElement(typeof(Enemy.EnemiesDetail))]
        [XmlElement(typeof(Hostage.HostagesDetail))]
        [XmlElement(typeof(Vehicle.VehiclesDetail))]
        [XmlElement(typeof(Helicopter.HelicoptersDetail))]
        [XmlElement(typeof(UAV.UAVsDetail))]
        [XmlElement(typeof(Camera.CamerasDetail))]
        [XmlElement(typeof(WalkerGear.WalkerGearsDetail))]
        [XmlElement(typeof(Animal.AnimalsDetail))]
        [XmlElement(typeof(Item.ItemsDetail))]
        [XmlElement(typeof(ActiveItem.ActiveItemsDetail))]
        [XmlElement(typeof(Model.ModelsDetail))]
        [XmlElement(typeof(GeoTrap.GeoTrapsDetail))]
        public List<ObjectsDetail> Details;

        public static Type[] GetAllDetailTypes()
        {
            Type[] AllDetailTypes = {
                typeof(Enemy.EnemiesDetail),
                typeof(Hostage.HostagesDetail),
                typeof(Vehicle.VehiclesDetail),
                typeof(Helicopter.HelicoptersDetail),
                typeof(UAV.UAVsDetail),
                typeof(Camera.CamerasDetail),
                typeof(WalkerGear.WalkerGearsDetail),
                typeof(Animal.AnimalsDetail),
                typeof(Item.ItemsDetail),
                typeof(ActiveItem.ActiveItemsDetail),
                typeof(Model.ModelsDetail),
                typeof(GeoTrap.GeoTrapsDetail),
            };
            return AllDetailTypes;
        }

        public ObjectsDetails()
        {
            Details = new List<ObjectsDetail>();
            foreach (Type type in GetAllDetailTypes())
            {
                ObjectsDetail questDetail = (ObjectsDetail)Activator.CreateInstance(type);
                Details.Add(questDetail);
            }
        }
    }
}
