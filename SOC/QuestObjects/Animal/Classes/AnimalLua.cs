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
        public static void GetMain(AnimalsDetail detail, MainLuaBuilder mainLua)
        {
            if (detail.animals.Count > 0)
            {
                mainLua.AddToQuestTable(BuildAnimalList(detail.animals));
                if (detail.animals.Any(animal => animal.target))
                {
                    CheckQuestAnimal checkAnimal = new CheckQuestAnimal(mainLua, detail.animalMetadata.objectiveType);
                    mainLua.AddToQuestTable(BuildAnimalTargetList(detail.animals));
                    mainLua.AddBaseQStep_MainMsgs(QStep_MainCommonMessages.animalTargetMessages);
                }
            }
        }

        private static Table BuildAnimalList(List<Animal> animals)
        {
            Table animalList = new Table("animalList");
            foreach (Animal animal in animals)
            {
                animalList.Add($@"
        {{
            animalName = ""{animal.GetObjectName()}"",
            animalType = ""{animal.typeID}"",
        }}");
            }
            return animalList;
        }

        private static Table BuildAnimalTargetList(List<Animal> animals)
        {
            Table targetAnimalList = new Table("targetAnimalList");

            StringBuilder animalTargetListBuilder = new StringBuilder(@"
        markerList = {");

            foreach (Animal animal in animals)
            {
                if (animal.target)
                    animalTargetListBuilder.Append($@"
            ""{animal.GetObjectName()}"",");
            }
            animalTargetListBuilder.Append(@"
        }");
            targetAnimalList.Add(animalTargetListBuilder.ToString());
            animalTargetListBuilder.Clear();

            animalTargetListBuilder.Append(@"
        nameList = {");
            foreach (Animal animal in animals)
            {
                if (animal.target)
                    animalTargetListBuilder.Append($@"
            ""{animal.GetObjectName()}"",");
            }
            animalTargetListBuilder.Append(@"
        }");
            targetAnimalList.Add(animalTargetListBuilder.ToString());

            return targetAnimalList;
        }
    }
}
