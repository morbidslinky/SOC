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
                    var methodPair = Create.TableEntry("methodPair",
                        Create.Table(
                            StaticObjectiveFunctions.IsTargetSetMessageIdForAnimal,
                            StaticObjectiveFunctions.TallyAnimalTargets
                        ), true
                    );

                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Create.TableEntry(
                            "ObjectiveTypeList",
                            Create.Table(Create.TableEntry("animalObjective", detail.animalMetadata.objectiveType))
                        ),
                        methodPair,
                        Create.TableEntry(
                            "CheckQuestMethodPairs",
                            Create.Table(Create.TableEntry(Create.Variable("qvars.methodPair.IsTargetSetMessageIdForAnimal"), Create.Variable("qvars.methodPair.TallyAnimalTargets"))),
                            true
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
                ChoiceKeyValues targetSenders = new ChoiceKeyValues("Animal Cluster Names (Targets)");

                foreach (string gameObjectName in detail.animals
                    .Where(o => o.isTarget)
                    .Select(o => o.GetObjectName()))
                {
                    targetSenders.Add(Create.String(gameObjectName));
                }

                questKeyValues.Add(targetSenders);
            }

            if (detail.animals.Count > 0)
            {
                ChoiceKeyValues allSenders = new ChoiceKeyValues("Animal Cluster Names");

                foreach (string gameObjectName in detail.animals.Select(o => o.GetObjectName()))
                {
                    allSenders.Add(Create.String(gameObjectName));
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
                    Create.TableEntry(
                        Create.Table(
                            Create.TableEntry("animalName", animal.GetObjectName()), 
                            Create.TableEntry("animalType", animal.typeID)
                        )
                    )
                );
            }

            return Create.TableEntry("animalList", animalList);
        }

        private static LuaTableEntry BuildAnimalTargetList(List<Animal> animals)
        {
            LuaTable targetAnimalList = new LuaTable();
            List<LuaTableEntry> nameList = new List<LuaTableEntry>();

            foreach (Animal animal in animals)
            {
                if (animal.isTarget)
                {
                    nameList.Add(Create.TableEntry(animal.GetObjectName()));
                }
            }

            targetAnimalList.Add(
                Create.TableEntry("markerList", Create.Table(nameList.ToArray())), 
                Create.TableEntry("nameList", Create.Table(nameList.ToArray()))
            );

            return Create.TableEntry("targetAnimalList", targetAnimalList);
        }
    }
}
