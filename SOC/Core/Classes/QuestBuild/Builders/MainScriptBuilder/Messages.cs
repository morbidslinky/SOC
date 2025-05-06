namespace SOC.Classes.Lua
{
    public class Messages
    {
        StrCode32Table StrCode32Table = new StrCode32Table();
        public const string TABLE_VAR_NAME = "questMessages";

        public Messages()
        {
            var blockScript = new Script(new StrCode32Event("Block", "StageBlockCurrentSmallBlockIndexUpdated", ""), Lua.TableEntry("StageBlockCurrentSmallBlockIndexUpdatedFunc", Lua.Function("")));
            StrCode32Table.Add(blockScript);
        }

        public LuaTableEntry Get()
        {
            return Lua.TableEntry("Messages", Lua.Function("return |[1|FUNCTION_CALL]|", Lua.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(TABLE_VAR_NAME))), true);
        }

        public LuaTable GetMessagesDefs()
        {
            return StrCode32Table.GetStrCode32DefinitionsTable(TABLE_VAR_NAME);
        }
    }
}
