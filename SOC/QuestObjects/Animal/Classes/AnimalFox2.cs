using SOC.Classes.Fox2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.QuestObjects.Animal
{
    class AnimalFox2
    {
        public static void AddQuestEntities(AnimalsDetail detail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Animal> animals = detail.animals;
            AnimalsMetadata meta = detail.animalMetadata;

            if (animals.Count() > 0)
            {
                foreach(string animalType in AnimalInfo.AnimalTypes)
                {
                    int typeCount = GetTypeCount(animals, animalType);
                    if (typeCount > 0)
                    {
                        GameObject animalObject = new GameObject(animalType + "_GameObject", dataSet, animalType, typeCount, typeCount);
                        entityList.Add(animalObject);
                        switch(animalType)
                        {
                            case "TppGoat":
                            case "TppNubian":
                            case "TppZebra":
                                TppAnimalParameter animalParam = new TppAnimalParameter(animalObject, GetFirstAnimalOfType(animals, animalType));
                                animalObject.SetParameter(animalParam);
                                entityList.Add(animalParam);
                                break;
                            case "TppWolf":
                            case "TppJackal":
                                TppWolfParameter wolfParam = new TppWolfParameter(animalObject, GetFirstAnimalOfType(animals, animalType));
                                animalObject.SetParameter(wolfParam);
                                entityList.Add(wolfParam);
                                break;
                            case "TppBear":
                                TppBearParameter bearParam = new TppBearParameter(animalObject, GetFirstAnimalOfType(animals, animalType));
                                animalObject.SetParameter(bearParam);
                                entityList.Add(bearParam);
                                break;
                        }
                    }
                }
                foreach (Animal animal in animals)
                {
                    GameObjectLocator locator = new GameObjectLocator(animal.GetObjectName(), dataSet, animal.typeID);
                    Transform transform = new Transform(locator, animal.position);
                    locator.SetTransform(transform);

                    entityList.Add(locator);
                    entityList.Add(transform);

                    switch (animal.typeID)
                    {
                        case "TppGoat":
                        case "TppNubian":
                        case "TppZebra":
                            TppAnimalLocatorParameter animalLocatorParam = new TppAnimalLocatorParameter(locator, animal.count);
                            locator.SetParameter(animalLocatorParam);
                            entityList.Add(animalLocatorParam);
                            break;
                        case "TppWolf":
                        case "TppJackal":
                            TppWolfLocatorParameter wolfLocatorParam = new TppWolfLocatorParameter(locator, animal.count);
                            locator.SetParameter(wolfLocatorParam);
                            entityList.Add(wolfLocatorParam);
                            break;
                        case "TppBear":
                            TppBearLocatorParameter bearLocatorParam = new TppBearLocatorParameter(locator, animal.count);
                            locator.SetParameter(bearLocatorParam);
                            entityList.Add(bearLocatorParam);
                            break;
                    }
                }
            }
        }

        private static string GetFirstAnimalOfType(List<Animal> animals, string animalType)
        {
            return animals.Where(animal => animal.typeID == animalType).Select(animal => animal.animal).First();
        }

        private static int GetTypeCount(List<Animal> animals, string animalType)
        {
            int count = 0;
            foreach(Animal animal in animals)
            {
                if (animal.typeID == animalType)
                    count += int.Parse(animal.count);
            }
            return count;
        }
    }
}
