namespace SOC.Classes.Lua
{
    public class Messages
    {
        LuaTable StrCode32Table = new LuaTable();

        public Messages()
        {
            StrCode32Table.AddOrSet(Lua.TableEntry(
                "Block", Lua.Table(
                    Lua.TableEntry("msg", "StageBlockCurrentSmallBlockIndexUpdated"),
                    Lua.TableEntry("func", Lua.Function("")))
                )
            );
        }

        public LuaTableEntry Get()
        {
            return Lua.TableEntry("Messages", Lua.Function("return |[0|function_call]|", Lua.FunctionCall("StrCode32Table", StrCode32Table)), true);
        }
    }
}
