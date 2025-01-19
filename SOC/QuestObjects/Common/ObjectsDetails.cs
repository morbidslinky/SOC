using SOC.Classes.Common;
using SOC.Forms.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SOC.QuestObjects.Common
{
    public class ObjectsDetails
    {
        public List<ObjectsDetail> details;

        public ObjectsDetails()
        {
            details = new List<ObjectsDetail>();
            foreach (Type type in DetailTypes.GetAllDetailTypes())
            {
                ObjectsDetail questDetail = (ObjectsDetail)Activator.CreateInstance(type);
                details.Add(questDetail);
            }
        }

        public ObjectsDetails(List<ObjectsDetail> questDetails)
        {
            details = questDetails.Select(detail => detail).ToList();
            var questDetailTypes = questDetails.Select(detail => detail.GetType());

            foreach (Type type in DetailTypes.GetAllDetailTypes())
            {
                if (!questDetailTypes.Contains(type))
                {
                    ObjectsDetail questDetail = (ObjectsDetail)Activator.CreateInstance(type);
                    details.Add(questDetail);
                }
            }
        }

        public void UpdateAllDetailsFromVisualizers()
        {
            details = details.Select(detail => detail.GetVisualizer().GetDetailFromControl()).ToList();
        }

        public void RefreshAllStubTexts()
        {
            foreach (ObjectsDetail detail in details)
            {
                if (detail is ObjectsDetailLocational)
                {
                    ((ObjectsDetailLocational)detail).RefreshStub();
                }
            }
        }

        public void RefreshAllPanels(SetupDetails setupDetails)
        {
            foreach (ObjectsDetail detail in details)
            {
                detail.UpdateDetailFromSetup(setupDetails);
                detail.RefreshPanel(setupDetails);
            }
        }

        internal void DisableVehicleBox()
        {
            foreach (ObjectsDetail detail in details)
            {
                if (detail is Vehicle.VehiclesDetail)
                {
                    ((ObjectsDetailLocational)detail).GetStub().DisableStub("Disabled On Mother Base");
                }
            }
        }

        internal void EnableVehicleBox()
        {
            foreach (ObjectsDetail detail in details)
            {
                if (detail is Vehicle.VehiclesDetail)
                {
                    ((ObjectsDetailLocational)detail).GetStub().EnableStub();
                }
            }
        }

        public LocationalDataStub[] GetLocationalStubs()
        {
            return details.OfType<ObjectsDetailLocational>().Select(detail => detail.GetStub()).ToArray();
        }

        public List<ObjectsDetail> GetQuestObjectDetails()
        {
            return details;
        }

        internal UserControl[] GetModulePanels()
        {
            return details.Select(manager => manager.GetVisualizer().detailControl).ToArray();
        }
    }
}
