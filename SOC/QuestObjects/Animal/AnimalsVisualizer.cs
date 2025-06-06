﻿using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Linq;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.UI;
using SOC.Forms.Pages;

namespace SOC.QuestObjects.Animal
{
    class AnimalsControlPanel : ObjectsDetailControlPanelLocational
    {
        public AnimalsControlPanel(LocationalDataStub stub, AnimalControl control) : base(stub, control, control.panelQuestBoxes) { }

        public override void DrawMetadata(ObjectsMetadata meta)
        {
            AnimalControl control = (AnimalControl)detailControl;
            control.SetMetadata((AnimalsMetadata)meta);
        }

        public override ObjectsMetadata GetMetadataFromControl()
        {
            return new AnimalsMetadata((AnimalControl)detailControl);
        }

        public override QuestObjectBox NewQuestObjectBox(QuestObject qObject)
        {
            return new AnimalBox((Animal)qObject);
        }

        public override ObjectsDetail NewDetail(ObjectsMetadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new AnimalsDetail(qObjects.Cast<Animal>().ToList(), (AnimalsMetadata)meta);
        }

        public override QuestObject NewQuestObject(Position pos, int index)
        {
            return new Animal(pos, index);
        }
    }
}
