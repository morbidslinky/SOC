using SOC.Classes.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SOC.QuestObjects.Animal
{
    class AnimalLua
    {
        public static void GetMain(AnimalsDetail detail, MainScriptBuilder mainLua)
        {
            if (detail.animals.Count > 0)
            {
                mainLua.QUEST_TABLE.Add(BuildAnimalList(detail.animals));
                if (detail.animals.Any(animal => animal.isTarget))
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

        internal static void GetScriptChoosableValueSets(AnimalsDetail detail, ChoiceKeyValuesList questKeyValues)
        {
            if (detail.animals.Any(o => o.isTarget))
            {
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Animal Clusters (Targets)");

                foreach (string gameObjectName in detail.animals
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.animals.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Animal Clusters");

                foreach (string gameObjectName in detail.animals.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Lua.String(gameObjectName));
                }

                questKeyValues.Add(allSenders);
            }
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
                if (animal.isTarget)
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
