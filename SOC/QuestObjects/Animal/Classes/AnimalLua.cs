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
                mainLua.QUEST_TABLE.AddOrSet(BuildAnimalList(detail.animals));
                if (detail.animals.Any(animal => animal.target))
                {
                    mainLua.QStep_Main.StrCode32Table.AddCommonDefinitions(
                        Lua.TableEntry(
                            "ObjectiveTypeList",
                            Lua.Table(Lua.TableEntry("animalObjective", detail.animalMetadata.objectiveType))
                        ),
                        StaticObjectiveFunctions.IsTargetSetMessageIdForAnimal,
                        StaticObjectiveFunctions.TallyAnimalTargets,
                        Lua.TableEntry(
                            "CheckQuestMethodPairs",
                            Lua.Table(Lua.TableEntry(Lua.Variable("qvars.IsTargetSetMessageIdForAnimal"), Lua.Variable("qvars.TallyAnimalTargets")))
                        ),
                        StaticObjectiveFunctions.CheckQuestAllTargetDynamicFunction
                    );
                    mainLua.QUEST_TABLE.AddOrSet(BuildAnimalTargetList(detail.animals));
                    mainLua.QStep_Main.StrCode32Table.Add(QStep_Main_CommonMessages.animalTargetMessages);
                }
            }
        }

        private static LuaTableEntry BuildAnimalList(List<Animal> animals)
        {
            LuaTable animalList = new LuaTable();

            foreach (Animal animal in animals)
            {
                animalList.AddOrSet(
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

            targetAnimalList.AddOrSet(
                Lua.TableEntry("markerList", Lua.Table(nameList.ToArray())), 
                Lua.TableEntry("nameList", Lua.Table(nameList.ToArray()))
            );

            return Lua.TableEntry("targetAnimalList", targetAnimalList);
        }
    }
}
