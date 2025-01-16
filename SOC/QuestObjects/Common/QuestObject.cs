using SOC.Core.Classes.InfiniteHeaven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Common
{
    public abstract class QuestObject
    {
        public abstract Position GetPosition();

        public abstract void SetPosition(Position pos);

        public abstract int GetID();

        public abstract string GetObjectName();
    }
}
