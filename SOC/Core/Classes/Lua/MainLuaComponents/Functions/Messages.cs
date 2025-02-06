using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    class Messages
    {
        public string ToLua(MainLua mainLua)
        {
            return @"
this.Messages = function()
  return
    StrCode32Table {
      Block = {
        {
          msg = ""StageBlockCurrentSmallBlockIndexUpdated"",
          func = function() end,
        },
      },
    }
end";
        }
    }
}
