using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class OnInitialize
    {
        public string ToLua(MainLua mainLua)
        {
            return @"
function this.OnInitialize()
	TppQuest.QuestBlockOnInitialize( this )
end";
        }
    }
}
