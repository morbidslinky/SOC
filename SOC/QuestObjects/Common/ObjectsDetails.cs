using SOC.Classes.Common;
using SOC.UI;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<ObjectsDetail> Details = new List<ObjectsDetail>();
    }
}
