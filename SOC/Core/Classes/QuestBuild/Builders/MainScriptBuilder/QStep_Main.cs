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
            QStep_Main_Table.AddOrSet(
                Lua.TableEntry("Messages", Lua.Function("return |[0|function_call]|", Lua.FunctionCall("StrCode32Table", StrCode32Table.ToStrCode32Table(strCode32TableVariableName))), false),
                Lua.TableEntry("OnEnter", OnEnterFunction.ToFunction()),
                Lua.TableEntry("OnLeave", OnLeaveFunction.ToFunction())
            );

            return Lua.TableEntry("QStep_Main", QStep_Main_Table, true);
        }
    }
}
public static class QStep_Main_CommonMessages
{
    static readonly StrCode32Script PlayerPickUpWeapon = new StrCode32Script(
        new StrCode32Event("Player", "OnPickUpWeapon", "", "playerIndex", "playerIndex"),
        LuaFunction.ToTableEntry(
            "PlayerPickUpWeaponClearCheck",
            new string[] { "playerIndex", "playerIndex" },
            " local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpDormant\", equipId); TppQuest.ClearWithSave(isClearType); ")
        );

    static readonly StrCode32Script PlayerPickUpPlaced = new StrCode32Script(
        new StrCode32Event("Player", "OnPickUpPlaced", "", "playerGameObjectId", "equipId", "index", "isPlayer"),
        LuaFunction.ToTableEntry(
            "PlayerPickUpPlacedClearCheck",
            new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
            " if TppPlaced.IsQuestBlock(index) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"PickUpActive\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script PlacedActivatePlaced = new StrCode32Script(
        new StrCode32Event("Placed", "OnActivatePlaced", "", "equipId", "index"),
        LuaFunction.ToTableEntry(
            "PlacedActivatePlacedClearCheck",
            new string[] { "equipId", "index" },
            " if TppPlaced.IsQuestBlock(index) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"Activate\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectDead = new StrCode32Script(
        new StrCode32Event("GameObject", "Dead", "", "gameObjectId", "gameObjectId01", "animalId"),
        LuaFunction.ToTableEntry(
            "GameObjectDeadClearCheck",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            " local isClearType = qvars.CheckQuestAllTargetDynamic(\"Dead\",gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

    static readonly StrCode32Script GameObjectFultonInfo = new StrCode32Script(
        new StrCode32Event("GameObject", "FultonInfo", "", "gameObjectId"),
        LuaFunction.ToTableEntry(
            "GameObjectFultonInfoClearCheck",
            new string[] { "gameObjectId" },
            " if mvars.fultonInfo ~= TppDefine.QUEST_CLEAR_TYPE.NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = TppDefine.QUEST_CLEAR_TYPE.NONE; "));

    static readonly StrCode32Script GameObjectFulton = new StrCode32Script(
        new StrCode32Event("GameObject", "Fulton", "", "gameObjectId", "animalId"),
        LuaFunction.ToTableEntry(
            "GameObjectFultonClearCheck",
            new string[] { "gameObjectId", "animalId" },
            " local isClearType = qvars.CheckQuestAllTargetDynamic(\"Fulton\", gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

    static readonly StrCode32Script GameObjectFultonFailed = new StrCode32Script(
        new StrCode32Event("GameObject", "FultonFailed", "", "gameObjectId", "locatorName", "locatorNameUpper", "failureType"),
        LuaFunction.ToTableEntry(
            "GameObjectFultonFailedClearCheck",
            new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
            " if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = qvars.CheckQuestAllTargetDynamic(\"FultonFailed\", gameObjectId, locatorName); TppQuest.ClearWithSave(isClearType); end;  "));

    static readonly StrCode32Script GameObjectPlacedIntoHeli = new StrCode32Script(
        new StrCode32Event("GameObject", "PlacedIntoVehicle", "", "gameObjectId", "vehicleGameObjectId"),
        LuaFunction.ToTableEntry(
            "GameObjectPlacedIntoHeliClearCheck",
            new string[] { "gameObjectId", "vehicleGameObjectId" },
            " if Tpp.IsHelicopter(vehicleGameObjectId) then local isClearType = qvars.CheckQuestAllTargetDynamic(\"InHelicopter\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectVehicleBroken = new StrCode32Script(
        new StrCode32Event("GameObject", "VehicleBroken", "", "gameObjectId", "state"),
        LuaFunction.ToTableEntry(
            "GameObjectVehicleBrokenClearCheck",
            new string[] { "gameObjectId", "state" },
            " if state == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"VehicleBroken\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

    static readonly StrCode32Script GameObjectLostControl = new StrCode32Script(
        new StrCode32Event("GameObject", "LostControl", "", "gameObjectId", "state"),
        LuaFunction.ToTableEntry(
            "GameObjectLostControlClearCheck",
            new string[] { "gameObjectId", "state" },
            " if state == StrCode32(\"End\") then local isClearType = qvars.CheckQuestAllTargetDynamic(\"LostControl\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

    public static readonly StrCode32Script[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] dormantItemTargetMessages = { PlayerPickUpWeapon };

    public static readonly StrCode32Script[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

    public static readonly StrCode32Script[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

    public static readonly StrCode32Script[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

    public static readonly StrCode32Script[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
}