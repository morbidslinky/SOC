using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public class QStep_Main
    {
        public List<StrCodeBlock> strCodes = new List<StrCodeBlock>();

        public void Add(StrCodeBlock _codeBlock)
        {
            foreach (StrCodeBlock codeBlock in strCodes)
            {
                if (codeBlock.Equals(_codeBlock))
                {
                    codeBlock.Add(_codeBlock.msgBlocks);
                    return;
                }
            }
            strCodes.Add(_codeBlock);
        }

        public bool Contains(StrCodeBlock _codeBlock)
        {
            var existingMsgFunctionPairs = new HashSet<(StrCodeMsgBlock, LuaFunctionOldFormat)>();

            foreach (StrCodeBlock codeBlock in strCodes)
            {
                foreach(StrCodeMsgBlock msgBlock in codeBlock.msgBlocks)
                {
                    foreach(LuaFunctionOldFormat luaFunction in msgBlock.functions)
                    {
                        existingMsgFunctionPairs.Add((msgBlock, luaFunction));
                    }
                }
            }

            foreach (StrCodeMsgBlock _msgBlock in _codeBlock.msgBlocks)
            {
                foreach (LuaFunctionOldFormat _luaFunction in _msgBlock.functions)
                {
                    if (existingMsgFunctionPairs.Contains((_msgBlock, _luaFunction)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public string ToLua(MainLuaBuilder mainLua)
        {
            return $@"
quest_step.QStep_Main = {{
  Messages = function( self )
    return StrCode32Table {{
        {string.Join(",", strCodes.Select(code => code.ToLua()))}
      }}
  end,
  OnEnter = function() end,
  OnLeave = function() end,
}}";
        }

    }

    public class StrCodeBlock
    {
        public string strCode;
        public List<StrCodeMsgBlock> msgBlocks = new List<StrCodeMsgBlock>();

        public StrCodeBlock(string _strCode, StrCodeMsgBlock _msgBlock)
        {
            strCode = _strCode; msgBlocks.Add(_msgBlock);
        }

        public StrCodeBlock(string _strCode, List<StrCodeMsgBlock> _msgBlocks)
        {
            strCode = _strCode; msgBlocks.AddRange(_msgBlocks);
        }

        public StrCodeBlock(string _strCode, string _name, string[] _msgArgs, params LuaFunctionOldFormat[] _functions)
        {
            strCode = _strCode; msgBlocks.Add(new StrCodeMsgBlock(_name, _msgArgs, _functions));
        }

        public StrCodeBlock(string _strCode, string _name, string _sender, string[] _msgArgs, params LuaFunctionOldFormat[] _functions)
        {
            strCode = _strCode; msgBlocks.Add(new StrCodeMsgBlock(_name, _sender, _msgArgs, _functions));
        }

        public void Add(List<StrCodeMsgBlock> _msgBlocks)
        {
            foreach (StrCodeMsgBlock msg in _msgBlocks)
            {
                this.Add(msg);
            }
        }

        public void Add(StrCodeMsgBlock _msgBlock)
        {
            foreach (StrCodeMsgBlock msgBlock in msgBlocks)
            {
                if (msgBlock.Equals(_msgBlock))
                {
                    msgBlock.AddFunctionCalls(_msgBlock.functions);
                    return;
                }
            }
            msgBlocks.Add(_msgBlock);
        }

        public bool Equals(StrCodeBlock _code)
        {
            return strCode.Equals(_code.strCode);
        }

        public string ToLua()
        {
            return $@"{strCode} = {{
            {string.Join(", ", msgBlocks.Select(msg => $"{{{msg.ToLua()}}}"))}
            }}";
        }
    }

    public class StrCodeMsgBlock
    {
        string msg;
        string sender;
        string[] msgArgs;
        public List<LuaFunctionOldFormat> functions;

        public StrCodeMsgBlock(string _name, string[] _msgArgs)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = new List<LuaFunctionOldFormat>();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = new List<LuaFunctionOldFormat>();
        }

        public StrCodeMsgBlock(string _name, string[] _msgArgs, LuaFunctionOldFormat[] _functions)
        {
            msg = _name; sender = ""; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public StrCodeMsgBlock(string _name, string _sender, string[] _msgArgs, LuaFunctionOldFormat[] _functions)
        {
            msg = _name; sender = _sender; msgArgs = _msgArgs; functions = _functions.ToList();
        }

        public void AddFunctionCalls(List<LuaFunctionOldFormat> calls)
        {
            functions.AddRange(calls);
        }

        public string ToLua()
        {
            return $@"
            msg = ""{msg}"", {(sender == "" ? "" : $@"
            sender = {sender}, ")}
            func = function({string.Join(", ", msgArgs)})
              {string.Join(" ", functions.Select(func => func.Call(msgArgs)))}
            end";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StrCodeMsgBlock other))
                return false;

            return msg.Equals(other.msg) && sender.Equals(other.sender);
        }

        public override int GetHashCode()
        {
            return msg.GetHashCode() + sender.GetHashCode();
        }
    }
}
