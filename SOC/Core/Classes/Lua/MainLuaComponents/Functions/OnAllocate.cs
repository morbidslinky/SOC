using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class OnAllocate
    {
        List<string> onTerminateCalls = new List<string>();

        public void AddOnTerminate(string call)
        {
            onTerminateCalls.Add(call);
        }

        public bool contains(string call)
        {
            return onTerminateCalls.Contains(call);
        }

        public string ToLua(MainLua mainLua)
        {
            return $@"
function this.OnAllocate()
  TppQuest.RegisterQuestStepList{{
    ""QStep_Start"",
    ""QStep_Main"",
    nil
  }}
  TppEnemy.OnAllocateQuestFova(this.QUEST_TABLE)
  TppQuest.RegisterQuestStepTable(quest_step)
  TppQuest.RegisterQuestSystemCallbacks{{
    OnActivate = function()
      TppEnemy.OnActivateQuest(this.QUEST_TABLE)
      TppAnimal.OnActivateQuest(this.QUEST_TABLE)
    end,
    OnDeactivate = function()
      TppEnemy.OnDeactivateQuest(this.QUEST_TABLE)
      TppAnimal.OnDeactivateQuest(this.QUEST_TABLE)
    end,
    OnOutOfAcitveArea = function() 
    end,
    OnTerminate = function(){BuildOnTerminate()}
    end,
  }}
  mvars.fultonInfo = NONE
end";
        }

        private string BuildOnTerminate()
        {
            AddOnTerminate("TppEnemy.OnTerminateQuest(this.QUEST_TABLE)");
            AddOnTerminate("TppAnimal.OnTerminateQuest(this.QUEST_TABLE)");

            StringBuilder terminateBuilder = new StringBuilder();
            foreach (string call in onTerminateCalls)
            {
                terminateBuilder.Append($@"
      {call}");
            }

            return terminateBuilder.ToString();
        }
    }
}
