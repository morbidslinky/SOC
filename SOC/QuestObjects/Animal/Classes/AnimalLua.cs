using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Animal
{
    class AnimalLua
    {
        public static void GetMain(AnimalsDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.animals.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildAnimalList(detail.animals));
                if (detail.animals.Any(animal => animal.target))
                {
                    var methodPair = Lua.TableEntry("methodPair",
                        Lua.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForAnimal,
                            StaticObjectiveFunctions.TallyAnimalTargets
                        )
                    );

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Lua.TableEntry(
                            "ObjectiveTypeList",
                            Lua.Table(Lua.TableEntry("animalObjective", detail.animalMetadata.objectiveType))
                        ),
                        methodPair,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.methodPair.IsTargetSetMessageIdForAnimal"), Lua.Variable("qvars.methodPair.TallyAnimalTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    mainLua.QUEST_TABLE.Add(BuildAnimalTargetList(detail.animals));
                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_TargetMessages.animalTargetMessages);
                }
            }
        }

        internal static void GetScriptChoosableValueSets(AnimalsDetail animalsDetail, ChoiceKeyValuesList questKeyValues)
        {
            //throw new NotImplementedException();
        }

        private static LuaTableEntry BuildAnimalList(List<Animal> animals)
        {
            LuaTable animalList = new LuaTable();

            foreach (Animal animal in animals)
            {
                animalList.Add(
                    Lua.TableEntry(
                        Lua.Table(
                            Lua.TableEntry("animalName", animal.GetObjectName()), 
                            Lua.TableEntry("animalType", animal.typeID)
                        )
                    )
                );
            }

            return Lua.TableEntry("animalList", animalList);
        }

        private static LuaTableEntry BuildAnimalTargetList(List<Animal> animals)
        {
            LuaTable targetAnimalList = new LuaTable();
            List<LuaTableEntry> nameList = new List<LuaTableEntry>();

            foreach (Animal animal in animals)
            {
                if (animal.target)
                {
                    nameList.Add(Lua.TableEntry(animal.GetObjectName()));
                }
            }

            targetAnimalList.Add(
                Lua.TableEntry("markerList", Lua.Table(nameList.ToArray())), 
                Lua.TableEntry("nameList", Lua.Table(nameList.ToArray()))
            );

            return Lua.TableEntry("targetAnimalList", targetAnimalList);
        }
    }
}
