using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOC.UI;
using SOC.QuestObjects.Common;

namespace SOC.QuestObjects.Animal
{
    public partial class AnimalBox : QuestObjectBox
    {
        public int ID;

        public AnimalBox(Animal qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;
            textBox_rot.Text = qObject.position.rotation.GetDegreeRotY();

            checkBox_target.Checked = qObject.target;

            comboBox_animal.Items.AddRange(AnimalInfo.animals);
            comboBox_animal.Text = qObject.animal;

            comboBox_typeID.Text = qObject.typeID;
            comboBox_count.Text = qObject.count;
        }

        private void comboBox_animal_selectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_count.Items.Clear();
            comboBox_typeID.Items.Clear();

            switch (comboBox_animal.Text)
            {
                case "Sheep":
                case "Cashmere_Goat":
                    comboBox_count.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppGoat", "TppNubian" });
                    break;
                case "Boer_Goat":
                case "Nubian":
                    comboBox_count.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppGoat", "TppNubian" });
                    break;

                case "Donkey":
                case "Zebra":
                case "Okapi":
                    comboBox_count.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppZebra" });
                    break;

                case "Wolf":
                    comboBox_count.Items.AddRange(new string[] { "1", "2", "3", "4" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppWolf", "TppJackal" });
                    break;

                case "Jackal":
                case "African_Wild_Dog":
                    comboBox_count.Items.AddRange(new string[] { "1", "2", "3", "4" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppWolf", "TppJackal", });
                    break;

                case "Bear":
                    comboBox_count.Items.AddRange(new string[] { "1" });
                    comboBox_typeID.Items.AddRange(new string[] { "TppBear" });
                    break;
            }
            comboBox_typeID.Text = AnimalInfo.getAnimalType(comboBox_animal.Text);
            comboBox_count.Text = "1";
        }

        public override QuestObject getQuestObject()
        {
            return new Animal(this);
        }
    }
}
