﻿using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Linq;

namespace SOC.QuestObjects.Animal
{
    public class AnimalDetail : Detail
    {
        public AnimalDetail() { }

        public AnimalDetail(List<Animal> animalList, AnimalMetadata meta)
        {
            animals = animalList; animalMetadata = meta;
        }

        [XmlElement]
        public AnimalMetadata animalMetadata { get; set; } = new AnimalMetadata();

        [XmlArray]
        public List<Animal> animals { get; set; } = new List<Animal>();

        public override Metadata GetMetadata()
        {
            return animalMetadata;
        }

        public override DetailManager GetNewManager()
        {
            return new AnimalManager(this);
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return animals.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            animals = qObjects.Cast<Animal>().ToList();
        }
    }

    public class Animal : QuestObject
    {
        public Animal() { }

        public Animal(AnimalBox box)
        {
            ID = box.ID;

            target = box.checkBox_target.Checked;
            count = box.comboBox_count.Text;
            animal = box.comboBox_animal.Text;
            typeID = box.comboBox_typeID.Text;
            position = new Position(new Coordinates(box.textBox_xcoord.Text, box.textBox_ycoord.Text, box.textBox_zcoord.Text), new Rotation(box.textBox_rot.Text));
        }

        public Animal(Position pos, int num)
        {
            position = pos; ID = num;
        }

        public override Position GetPosition()
        {
            return position;
        }

        public override void SetPosition(Position pos)
        {
            position = pos;
        }

        public override int GetID()
        {
            return ID;
        }

        public override string GetObjectName()
        {
            return "Animal_Cluster_" + ID;
        }

        [XmlElement]
        public bool target { get; set; } = false;

        [XmlAttribute]
        public int ID { get; set; } = 0;

        [XmlElement]
        public string count { get; set; } = "1";

        [XmlElement]
        public string animal { get; set; } = "Sheep";

        [XmlElement]
        public string typeID { get; set; } = "TppGoat";

        [XmlElement]
        public Position position { get; set; } = new Position(new Coordinates(), new Rotation());
    }

    public class AnimalMetadata : Metadata
    {
        public AnimalMetadata() { }

        public AnimalMetadata(AnimalControl control)
        {
            objectiveType = control.comboBox_ObjType.Text;
        }

        [XmlAttribute]
        public string objectiveType { get; set; } = "ELIMINATE";

    }
}
