using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class OnTerminate
    {
        public string ToLua(MainLua mainLua)
        {
            return @"
function this.OnTerminate()
	TppQuest.QuestBlockOnTerminate(this)
end";
        }
    }
}
