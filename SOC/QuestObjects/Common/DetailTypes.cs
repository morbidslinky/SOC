using System;

namespace SOC.QuestObjects.Common
{
    internal class DetailTypes
    {
        public static Type[] GetAllDetailTypes() // Attach modules here
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
    }
}
