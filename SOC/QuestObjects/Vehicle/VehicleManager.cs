﻿using SOC.Classes.Fox2;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms.Pages;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Vehicle
{
    class VehicleManager : DetailManager
    {
        static LocationalDataStub vehicleStub = new LocationalDataStub("Heavy Vehicle Locations");

        static VehiclePanel vehiclePanel = new VehiclePanel();

        static VehicleVisualizer vehicleVisualizer = new VehicleVisualizer(vehicleStub, vehiclePanel);

        private VehicleDetails vehicleDetails;

        public VehicleManager(VehicleDetails vehicleDets) : base(vehicleDets, vehicleVisualizer)
        {
            vehicleDetails = vehicleDets;
        }

        public override void AddFox2Entities(ref List<Fox2EntityClass> entityList)
        {
            throw new NotImplementedException();
        }
    }
}
