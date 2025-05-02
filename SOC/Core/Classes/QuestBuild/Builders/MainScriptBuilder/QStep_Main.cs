using SOC.Classes.Lua;

namespace SOC.Classes.Lua
{
    public class QStep_Main
    {
        public StrCode32Table StrCode32Table = new StrCode32Table();

        LuaTable QStep_Main_Table = new LuaTable();

        public LuaFunctionBuilder OnEnterFunction = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnLeaveFunction = new LuaFunctionBuilder();

        public QStep_Main()
        {
            OnEnterFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.Text("QStep_Main OnEnter")));
            OnLeaveFunction.AppendLuaValue(Lua.FunctionCall(Lua.TableIdentifier("Fox", "Log"), Lua.Text("QStep_Main OnLeave")));
        }

        public LuaTableEntry Get(string strCode32TableVariableName)
        {
            QStep_Main_Table.Add(
                Lua.TableEntry("Messages", Lua.Function("return |[0|FUNCTION_CALL]|", Lua.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(strCode32TableVariableName))), false),
                Lua.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Lua.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Lua.TableEntry("QStep_Main", QStep_Main_Table, true);
        }
    }
}

public static class QStep_Main_TargetMessages
{
    static readonly StrCode32Script PlayerPickUpWeapon = new StrCode32Script(
        new StrCode32Event("Player", "OnPickUpWeapon", ""),
        LuaFunction.ToTableEntry(
            "PlayerPickUpWeaponClearCheck", StrCode32Event.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpDormant\", {StrCode32Event.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); ")
        );

    static readonly StrCode32Script PlayerPickUpPlaced = new StrCode32Script(
        new StrCode32Event("Player", "OnPickUpPlaced", ""),
        LuaFunction.ToTableEntry(
            "PlayerPickUpPlacedClearCheck", StrCode32Event.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32Event.DefaultParameters[2]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpActive\", {StrCode32Event.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script PlacedActivatePlaced = new StrCode32Script(
        new StrCode32Event("Placed", "OnActivatePlaced", ""),
        LuaFunction.ToTableEntry(
            "PlacedActivatePlacedClearCheck", StrCode32Event.DefaultParameters,
            $"if TppPlaced.IsQuestBlock({StrCode32Event.DefaultParameters[1]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"Activate\", {StrCode32Event.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectDead = new StrCode32Script(
        new StrCode32Event("GameObject", "Dead", ""),
        LuaFunction.ToTableEntry(
            "GameObjectDeadClearCheck", StrCode32Event.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"Dead\", {StrCode32Event.DefaultParameters[0]}, {StrCode32Event.DefaultParameters[2]}); TppQuest.ClearWithSave(isClearType); "));

    static readonly StrCode32Script GameObjectFultonInfo = new StrCode32Script(
        new StrCode32Event("GameObject", "FultonInfo", ""),
        LuaFunction.ToTableEntry(
            "GameObjectFultonInfoClearCheck", StrCode32Event.DefaultParameters,
            $"if mvars.fultonInfo ~= TppDefine.QUEST_CLEAR_TYPE.NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = TppDefine.QUEST_CLEAR_TYPE.NONE; "));

    static readonly StrCode32Script GameObjectFulton = new StrCode32Script(
        new StrCode32Event("GameObject", "Fulton", ""),
        LuaFunction.ToTableEntry(
            "GameObjectFultonClearCheck", StrCode32Event.DefaultParameters,
            $"local isClearType = qvars.CheckQuestAllTargetDynamic(\"Fulton\", {StrCode32Event.DefaultParameters[0]}, {StrCode32Event.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); "));

    static readonly StrCode32Script GameObjectFultonFailed = new StrCode32Script(
        new StrCode32Event("GameObject", "FultonFailed", ""),
        LuaFunction.ToTableEntry(
            "GameObjectFultonFailedClearCheck", StrCode32Event.DefaultParameters,
            $"if {StrCode32Event.DefaultParameters[3]} == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = qvars.CheckQuestAllTargetDynamic(\"FultonFailed\", {StrCode32Event.DefaultParameters[0]}, {StrCode32Event.DefaultParameters[1]}); TppQuest.ClearWithSave(isClearType); end;  "));

    static readonly StrCode32Script GameObjectPlacedIntoHeli = new StrCode32Script(
        new StrCode32Event("GameObject", "PlacedIntoVehicle", ""),
        LuaFunction.ToTableEntry(
            "GameObjectPlacedIntoHeliClearCheck", StrCode32Event.DefaultParameters,
            $"if Tpp.IsHelicopter({StrCode32Event.DefaultParameters[1]}) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"InHelicopter\", {StrCode32Event.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectVehicleBroken = new StrCode32Script(
        new StrCode32Event("GameObject", "VehicleBroken", ""),
        LuaFunction.ToTableEntry(
            "GameObjectVehicleBrokenClearCheck", StrCode32Event.DefaultParameters,
            $"if {StrCode32Event.DefaultParameters[1]} == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"VehicleBroken\", {StrCode32Event.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectLostControl = new StrCode32Script(
        new StrCode32Event("GameObject", "LostControl", ""),
        LuaFunction.ToTableEntry(
            "GameObjectLostControlClearCheck", StrCode32Event.DefaultParameters,
            $"if {StrCode32Event.DefaultParameters[1]} == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"LostControl\", {StrCode32Event.DefaultParameters[0]}); TppQuest.ClearWithSave(isClearType); end; "));

    public static readonly StrCode32Script[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] dormantItemTargetMessages = { PlayerPickUpWeapon };

    public static readonly StrCode32Script[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

    public static readonly StrCode32Script[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

    public static readonly StrCode32Script[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
}