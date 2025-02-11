using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Lua
{
    public static class QStep_MainCommonMessages
    {

        static readonly StrCodeBlock PlayerPickUpWeapon = new StrCodeBlock(
            "Player",
            "OnPickUpWeapon",
            new string[] { "playerIndex", "playerIndex" },
            new LuaFunctionOldFormat(
                "PlayerPickUpWeaponClearCheck",
                new string[] { "playerIndex", "playerIndex" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpDormant\", equipId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock PlayerPickUpPlaced = new StrCodeBlock(
            "Player",
            "OnPickUpPlaced",
            new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
            new LuaFunctionOldFormat(
                "PlayerPickUpPlacedClearCheck",
                new string[] { "playerGameObjectId", "equipId", "index", "isPlayer" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"PickUpActive\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock PlacedActivatePlaced = new StrCodeBlock(
            "Placed",
            "OnActivatePlaced",
            new string[] { "equipId", "index" },
            new LuaFunctionOldFormat(
                "PlacedActivatePlacedClearCheck",
                new string[] { "equipId", "index" },
                " if TppPlaced.IsQuestBlock(index) then local isClearType = this.CheckQuestAllTargetDynamic(\"Activate\", equipId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectDead = new StrCodeBlock(
            "GameObject",
            "Dead",
            new string[] { "gameObjectId", "gameObjectId01", "animalId" },
            new LuaFunctionOldFormat(
                "GameObjectDeadClearCheck",
                new string[] { "gameObjectId", "gameObjectId01", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Dead\",gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonInfo = new StrCodeBlock(
            "GameObject",
            "FultonInfo",
            new string[] { "gameObjectId"},
            new LuaFunctionOldFormat(
                "GameObjectFultonInfoClearCheck",
                new string[] { "gameObjectId" },
                " if mvars.fultonInfo ~= NONE then TppQuest.ClearWithSave(mvars.fultonInfo) end; mvars.fultonInfo = NONE; "));

        static readonly StrCodeBlock GameObjectFulton = new StrCodeBlock(
            "GameObject",
            "Fulton",
            new string[] { "gameObjectId", "animalId" },
            new LuaFunctionOldFormat(
                "GameObjectFultonClearCheck",
                new string[] { "gameObjectId", "animalId" },
                " local isClearType = this.CheckQuestAllTargetDynamic(\"Fulton\", gameObjectId, animalId); TppQuest.ClearWithSave(isClearType); "));

        static readonly StrCodeBlock GameObjectFultonFailed = new StrCodeBlock(
            "GameObject",
            "FultonFailed",
            new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
            new LuaFunctionOldFormat(
                "GameObjectFultonFailedClearCheck",
                new string[] { "gameObjectId", "locatorName", "locatorNameUpper", "failureType" },
                " if failureType == TppGameObject.FULTON_FAILED_TYPE_ON_FINISHED_RISE then local isClearType = this.CheckQuestAllTargetDynamic(\"FultonFailed\", gameObjectId, locatorName); TppQuest.ClearWithSave(isClearType); end;  "));

        static readonly StrCodeBlock GameObjectPlacedIntoHeli = new StrCodeBlock(
            "GameObject",
            "PlacedIntoVehicle",
            new string[] { "gameObjectId", "vehicleGameObjectId" },
            new LuaFunctionOldFormat(
                "GameObjectPlacedIntoHeliClearCheck",
                new string[] { "gameObjectId", "vehicleGameObjectId" },
                " if Tpp.IsHelicopter(vehicleGameObjectId) then local isClearType = this.CheckQuestAllTargetDynamic(\"InHelicopter\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectVehicleBroken = new StrCodeBlock(
            "GameObject",
            "VehicleBroken",
            new string[] { "gameObjectId", "state" },
            new LuaFunctionOldFormat(
                "GameObjectVehicleBrokenClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"VehicleBroken\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        static readonly StrCodeBlock GameObjectLostControl = new StrCodeBlock(
            "GameObject",
            "LostControl",
            new string[] { "gameObjectId", "state" },
            new LuaFunctionOldFormat(
                "GameObjectLostControlClearCheck",
                new string[] { "gameObjectId", "state" },
                " if state == StrCode32(\"End\") then local isClearType = this.CheckQuestAllTargetDynamic(\"LostControl\", gameObjectId); TppQuest.ClearWithSave(isClearType); end; "));

        public static readonly StrCodeBlock[] allCommonMessages = { PlayerPickUpWeapon, PlayerPickUpPlaced, PlacedActivatePlaced, GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] genericTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectPlacedIntoHeli, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] dormantItemTargetMessages = { PlayerPickUpWeapon };

        public static readonly StrCodeBlock[] activeItemTargetMessages = { PlayerPickUpPlaced, PlacedActivatePlaced };

        public static readonly StrCodeBlock[] mechaCaptureTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed, GameObjectVehicleBroken };

        public static readonly StrCodeBlock[] mechaNoCaptureTargetMessages = { GameObjectDead, GameObjectVehicleBroken, GameObjectLostControl };

        public static readonly StrCodeBlock[] animalTargetMessages = { GameObjectDead, GameObjectFultonInfo, GameObjectFulton, GameObjectFultonFailed };
    }
}
