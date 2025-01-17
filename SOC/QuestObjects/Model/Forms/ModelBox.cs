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
using System.IO;

namespace SOC.QuestObjects.Model
{
    public partial class ModelBox : QuestObjectBox
    {
        public int ID;

        public ModelBox(Model qObject)
        {
            InitializeComponent();
            ID = qObject.ID;
            groupBox_main.Text = qObject.GetObjectName();

            textBox_xcoord.Text = qObject.position.coords.xCoord;
            textBox_ycoord.Text = qObject.position.coords.yCoord;
            textBox_zcoord.Text = qObject.position.coords.zCoord;

            textBox_xrot.Text = qObject.position.rotation.xRot;
            textBox_yrot.Text = qObject.position.rotation.yRot;
            textBox_zrot.Text = qObject.position.rotation.zRot;
            textBox_wrot.Text = qObject.position.rotation.wRot;

            comboBox_model.Items.AddRange(getModelList());

            if (comboBox_model.Items.Contains(qObject.model))
                comboBox_model.Text = qObject.model;
            else if (comboBox_model.Items.Count > 0)
                comboBox_model.SelectedIndex = 0;

            if (checkBox_collision.Enabled)
                checkBox_collision.Checked = qObject.collision;
        }

        private string[] getModelList()
        {

            string[] FileNames = Directory.GetFiles(ModelAssets.modelAssetsPath, "*.fmdl");
            for (int i = 0; i < FileNames.Length; i++)
            {
                FileNames[i] = Path.GetFileNameWithoutExtension(FileNames[i]);
            }
            return FileNames;
        }

        private bool hasGeom()
        {
            if (!string.IsNullOrEmpty(comboBox_model.Text))
            {
                string[] geomNames = Directory.GetFiles(ModelAssets.modelAssetsPath, "*.geom");
                for (int i = 0; i < geomNames.Length; i++)
                {
                    if (geomNames[i].Contains(comboBox_model.Text + ".geom"))
                        return true;
                }
            }
            return false;
        }

        private void m_comboBox_model_selectedIndexChanged(object sender, EventArgs e)
        {
            if (!hasGeom() && !string.IsNullOrEmpty(comboBox_model.Text))
            {
                DisableCollisionCheckBox("Missing .Geom");
            }
            else
            {
                EnableCollisionCheckBox();
            }

        }

        private void EnableCollisionCheckBox()
        {
            checkBox_collision.Enabled = true;
            checkBox_collision.Text = "Enable Collision";
        }

        private void DisableCollisionCheckBox(string reason)
        {
            checkBox_collision.Text = $"Enable Collision ({reason})";
            checkBox_collision.Checked = false;
            checkBox_collision.Enabled = false;
        }

        public override QuestObject getQuestObject()
        {
            return new Model(this);
        }
    }
}
