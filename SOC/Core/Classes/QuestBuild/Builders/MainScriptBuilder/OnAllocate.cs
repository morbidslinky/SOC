namespace SOC.Classes.Lua
{
    public class OnAllocate
    {
        public LuaFunctionBuilder OnActivate = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnDeactivate = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnOutOfAcitveArea = new LuaFunctionBuilder();
        public LuaFunctionBuilder OnTerminate = new LuaFunctionBuilder();

        public OnAllocate()
        {
            OnActivate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppEnemy", "OnActivateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnActivate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppAnimal", "OnActivateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnDeactivate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppEnemy", "OnDeactivateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnDeactivate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppAnimal", "OnDeactivateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnTerminate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppEnemy", "OnTerminateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnTerminate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppAnimal", "OnTerminateQuest"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));
        }
        public LuaTableEntry Get()
        {
            var registerQuestSystemCallbacks = Lua.Table(
                OnActivate.ToTableEntry("OnActivate"),
                OnDeactivate.ToTableEntry("OnDeactivate"),
                OnOutOfAcitveArea.ToTableEntry("OnOutOfAcitveArea"),
                OnTerminate.ToTableEntry("OnTerminate")
            );

            LuaFunctionBuilder OnAllocate = new LuaFunctionBuilder();
            OnAllocate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppQuest", "RegisterQuestStepList"),
                    Lua.Table(new LuaValue[] { Lua.String("QStep_Start"), Lua.String("QStep_Main"), Lua.Nil() })));

            OnAllocate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppEnemy", "OnAllocateQuestFova"),
                    Lua.TableIdentifier("this", "QUEST_TABLE")));

            OnAllocate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppQuest", "RegisterQuestStepTable"),
                    Lua.TableIdentifier("this", "quest_step")));

            OnAllocate.AppendLuaValue(
                Lua.FunctionCall(
                    Lua.TableIdentifier("TppQuest", "RegisterQuestSystemCallbacks"),
                    registerQuestSystemCallbacks));

            OnAllocate.AppendAssignment(
                Lua.TableIdentifier("mvars", "fultonInfo"),
                Lua.TableIdentifier("TppDefine", "QUEST_CLEAR_TYPE", "NONE"));

            return OnAllocate.ToTableEntry("OnAllocate", true);
        }
    }
}
