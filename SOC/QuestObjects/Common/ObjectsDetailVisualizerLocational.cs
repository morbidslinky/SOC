﻿using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.Forms.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOC.QuestObjects.Common
{
    public abstract class ObjectsDetailControlPanelLocational : ObjectsDetailControlPanel
    {
        public LocationalDataStub detailStub { get; }

        public ObjectsDetailControlPanelLocational(LocationalDataStub stub, UserControl control, FlowLayoutPanel panel) : base(control, panel)
        {
            detailStub = stub;
        }

        public void DrawStubText(ObjectsDetail detail)
        {
            List<Position> posList = new List<Position>();

            foreach (QuestObject qObject in detail.GetQuestObjects())
            {
                posList.Add(qObject.GetPosition());
            }
            detailStub.SetStubText(new IHLogPositions(posList));
        }

        public override void SetDetailsFromSetup(ObjectsDetail detail, SetupDetails setup)
        {
            List<Position> stubPositions = detailStub.GetStubLocations().GetPositions();
            List<QuestObject> qObjects = detail.GetQuestObjects();
            int positionCount = stubPositions.Count;
            int objectCount = qObjects.Count;

            for (int i = 0; i < positionCount; i++)
            {
                if (i >= objectCount) // add
                {
                    qObjects.Add(NewQuestObject(stubPositions[i], i));
                }
                else // modify
                {
                    qObjects[i].SetPosition(stubPositions[i]);
                }
            }

            for (int i = objectCount - 1; i >= positionCount; i--) //remove
            {
                qObjects.RemoveAt(i);
            }

            detail.SetQuestObjects(qObjects);
        }

        public abstract QuestObject NewQuestObject(Position pos, int index);

    }
}
