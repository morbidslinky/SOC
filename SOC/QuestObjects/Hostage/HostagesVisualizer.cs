using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using SOC.Forms.Pages;
using SOC.UI;
using SOC.Classes.Common;

namespace SOC.QuestObjects.Hostage
{
    class HostagesControlPanel : ObjectsDetailControlPanelLocational
    {
        public HostagesControlPanel(LocationalDataStub hostageStub, HostageControl hostageControl) : base(hostageStub, hostageControl, hostageControl.panelQuestBoxes)
        {
            hostageControl.comboBox_Body.SelectedIndexChanged += OnBodyIndexChanged;
        }

        string[] bodyNames = new string[0];
        string cpName = "NONE";

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            HostageControl hostageControl = (HostageControl)detailControl;
            hostageControl.SetMetadata((HostageMetadata)meta, bodyNames, cpName);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new HostageMetadata((HostageControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new HostageBox((Hostage)qObject, (HostageMetadata)GetMetadataFromControl());
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new HostagesDetail(qObjects.Cast<Hostage>().ToList(), (HostageMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position objectPosition, int objectID)
        {
            return new Hostage(objectPosition, objectID);
        }

        public override void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails setup)
        {
            base.SetDetailsFromSetup(detail, setup);
            if (LoadAreas.isMtbs(setup.locationID))
            {
                bodyNames = NPCBodyInfo.BodyInfoArray.Where(bodyEntry => bodyEntry.hasface).Select(BodyEntry => BodyEntry.Name).ToArray();
            }
            else
            {
                bodyNames = NPCBodyInfo.BodyInfoArray.Select(bodyEntry => bodyEntry.Name).ToArray();
            }
            cpName = setup.CPName;
        }

        private void OnBodyIndexChanged(object sender, EventArgs e)
        {
            RefreshHostageLanguage();
        }

        private void RefreshHostageLanguage()
        {
            HostageMetadata meta = (HostageMetadata)GetMetadataFromControl();
            foreach (HostageBox hBox in flowPanel.Controls.OfType<HostageBox>())
            {
                hBox.RefreshLanguage(meta.hostageBodyName);
            }
        }
    }
}
