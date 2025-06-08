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
                Create.FunctionCall(
                    Create.TableIdentifier("TppEnemy", "OnActivateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnActivate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppAnimal", "OnActivateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnDeactivate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppEnemy", "OnDeactivateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnDeactivate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppAnimal", "OnDeactivateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnTerminate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppEnemy", "OnTerminateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnTerminate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppAnimal", "OnTerminateQuest"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));
        }
        public LuaTableEntry Get()
        {
            var registerQuestSystemCallbacks = Create.Table(
                OnActivate.ToTableEntry("OnActivate"),
                OnDeactivate.ToTableEntry("OnDeactivate"),
                OnOutOfAcitveArea.ToTableEntry("OnOutOfAcitveArea"),
                OnTerminate.ToTableEntry("OnTerminate")
            );

            LuaFunctionBuilder OnAllocate = new LuaFunctionBuilder();
            OnAllocate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppQuest", "RegisterQuestStepList"),
                    Create.Table(new LuaValue[] { Create.String("QStep_Start"), Create.String("QStep_Main"), Create.Nil() })));

            OnAllocate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppEnemy", "OnAllocateQuestFova"),
                    Create.TableIdentifier("this", "QUEST_TABLE")));

            OnAllocate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppQuest", "RegisterQuestStepTable"),
                    Create.TableIdentifier("this", "quest_step")));

            OnAllocate.AppendLuaValue(
                Create.FunctionCall(
                    Create.TableIdentifier("TppQuest", "RegisterQuestSystemCallbacks"),
                    registerQuestSystemCallbacks));

            OnAllocate.AppendAssignment(
                Create.TableIdentifier("mvars", "fultonInfo"),
                Create.TableIdentifier("TppDefine", "QUEST_CLEAR_TYPE", "NONE"));

            return OnAllocate.ToTableEntry("OnAllocate", true);
        }
    }
}
