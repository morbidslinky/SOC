﻿using SOC.QuestComponents;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static SOC.QuestComponents.GameObjectInfo;
using SOC.Forms.Pages.QuestBoxes;

namespace SOC.UI
{
    public partial class Details : UserControl
    {
        List<GroupBox> detailLists;
        List<QuestBox> questEnemyBoxes;
        List<QuestBox> CPEnemyBoxes;
        List<QuestBox> hostageBoxes;
        List<QuestBox> vehicleBoxes;
        List<QuestBox> itemBoxes;
        List<QuestBox> modelBoxes;
        List<QuestBox> activeItemBoxes;
        List<QuestBox> animalBoxes;
        List<QuestBox> heliBoxes;
        List<QuestBox> walkerBoxes;
        int dynamicPanelWidth = 0;

        public Details()
        {
            InitializeComponent();
            DoubleBuffered = true;
            detailLists = new List<GroupBox>();
            questEnemyBoxes = new List<QuestBox>();
            CPEnemyBoxes = new List<QuestBox>();
            hostageBoxes = new List<QuestBox>();
            vehicleBoxes = new List<QuestBox>();
            itemBoxes = new List<QuestBox>();
            modelBoxes = new List<QuestBox>();
            activeItemBoxes = new List<QuestBox>();
            animalBoxes = new List<QuestBox>();
            heliBoxes = new List<QuestBox>();
            walkerBoxes = new List<QuestBox>();

            foreach (BodyInfoEntry infoEntry in BodyInfo.BodyInfoArray)
            {
                this.comboBox_Body.Items.Add(infoEntry.bodyName);
            }
            comboBox_Body.Text = "AFGH_HOSTAGE";
        }

        internal void clearPanel(Panel panel, List<QuestBox> objectlist)
        {
            foreach (QuestBox qbox in objectlist)
            {
                panel.Controls.Remove(qbox.getGroupBoxMain());
            }
        }

        public void ResetAllPanels()
        {
            clearPanel(panelQuestEnemyDet, questEnemyBoxes);
            clearPanel(panelCPEnemyDet, CPEnemyBoxes);
            clearPanel(panelHosDet, hostageBoxes);
            clearPanel(panelVehDet, vehicleBoxes);
            clearPanel(panelAnimalDet, animalBoxes);
            clearPanel(panelItemDet, itemBoxes);
            clearPanel(panelAcItDet, activeItemBoxes);
            clearPanel(panelStMdDet, modelBoxes);
            clearPanel(panelHeliDet, heliBoxes);
            clearPanel(panelWalkerDet, walkerBoxes);
            questEnemyBoxes = new List<QuestBox>();
            CPEnemyBoxes = new List<QuestBox>();
            hostageBoxes = new List<QuestBox>();
            vehicleBoxes = new List<QuestBox>();
            itemBoxes = new List<QuestBox>();
            modelBoxes = new List<QuestBox>();
            activeItemBoxes = new List<QuestBox>();
            animalBoxes = new List<QuestBox>();
            heliBoxes = new List<QuestBox>();
            walkerBoxes = new List<QuestBox>();
            EnemyInfo.armorCount = 0;
            EnemyInfo.balaCount = 0;
            EnemyInfo.zombieCount = 0;
        }

        public void LoadEntityLists(CP enemyCP, QuestEntities questDetails, string[] frtRouteNames, int locId)
        {
            ShiftVisibilities(true);
            SetObjectiveTypes(questDetails); SetEnemySubType(questDetails, locId); SetHostageBodies(questDetails, locId);
            h_checkBox_intrgt.Checked = questDetails.canInter;

            //
            // Quest-Specific Soldiers
            //
            Panel currentPanel = panelQuestEnemyDet;
            currentPanel.AutoScroll = false;
            foreach (Enemy questEnemy in questDetails.questEnemies)
            {
                EnemyBox questEnemyBox = new EnemyBox(questEnemy, enemyCP, frtRouteNames, locId);
                questEnemyBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(questEnemyBox.getGroupBoxMain());
                questEnemyBoxes.Add(questEnemyBox);
            }
            currentPanel.AutoScroll = true;
            //
            // CP-Specific soldiers
            //
            currentPanel = panelCPEnemyDet;
            currentPanel.AutoScroll = false;
            foreach (Enemy cpEnemy in questDetails.cpEnemies)
            {
                EnemyBox cpEnemyBox = new EnemyBox(cpEnemy, enemyCP, frtRouteNames, locId);
                cpEnemyBox.BuildObject(currentPanel.Width);
                cpEnemyBox.e_label_spawn.Text = "Customize:"; cpEnemyBox.e_label_spawn.Left = 26;
                currentPanel.Controls.Add(cpEnemyBox.getGroupBoxMain());
                CPEnemyBoxes.Add(cpEnemyBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Helicopter
            //
            currentPanel = panelHeliDet;
            currentPanel.AutoScroll = false;
            foreach (Helicopter heli in questDetails.enemyHelicopters)
            {
                HelicopterBox heliBox = new HelicopterBox(heli, enemyCP, frtRouteNames);
                heliBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(heliBox.getGroupBoxMain());
                heliBoxes.Add(heliBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Hostages
            //
            currentPanel = panelHosDet;
            currentPanel.AutoScroll = false;
            foreach (Hostage hostage in questDetails.hostages)
            {
                HostageBox hostageBox = new HostageBox(hostage, questDetails.hostageBodyIndex);
                hostageBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(hostageBox.getGroupBoxMain());
                hostageBoxes.Add(hostageBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Heavy Vehicles
            //
            currentPanel = panelVehDet;
            currentPanel.AutoScroll = false;
            foreach (Vehicle vehicle in questDetails.vehicles)
            {
                VehicleBox vehiclebox = new VehicleBox(vehicle);
                vehiclebox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(vehiclebox.getGroupBoxMain());
                vehicleBoxes.Add(vehiclebox);
            }
            currentPanel.AutoScroll = true;
            //
            // Walker Gears
            //
            currentPanel = panelWalkerDet;
            currentPanel.AutoScroll = false;
            foreach (WalkerGear walker in questDetails.walkerGears)
            {
                WalkerGearBox walkerBox = new WalkerGearBox(walker);
                walkerBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(walkerBox.getGroupBoxMain());
                walkerBoxes.Add(walkerBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Animal Clusters
            //
            currentPanel = panelAnimalDet;
            currentPanel.AutoScroll = false;
            foreach (Animal animal in questDetails.animals)
            {
                AnimalBox anibox = new AnimalBox(animal);
                anibox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(anibox.getGroupBoxMain());
                animalBoxes.Add(anibox);
            }
            currentPanel.AutoScroll = true;
            //
            // Dormant Items
            //
            currentPanel = panelItemDet;
            currentPanel.AutoScroll = false;
            foreach (Item item in questDetails.items)
            {
                ItemBox itemBox = new ItemBox(item);
                itemBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(itemBox.getGroupBoxMain());
                itemBoxes.Add(itemBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Active Items
            //
            currentPanel = panelAcItDet;
            currentPanel.AutoScroll = false;
            foreach (ActiveItem acitem in questDetails.activeItems)
            {
                ActiveItemBox activeItemBox = new ActiveItemBox(acitem);
                activeItemBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(activeItemBox.getGroupBoxMain());
                activeItemBoxes.Add(activeItemBox);
            }
            currentPanel.AutoScroll = true;
            //
            // Models
            //
            currentPanel = panelStMdDet;
            currentPanel.AutoScroll = false;
            foreach (Model model in questDetails.models)
            {
                ModelBox modelBox = new ModelBox(model);
                modelBox.BuildObject(currentPanel.Width);
                currentPanel.Controls.Add(modelBox.getGroupBoxMain());
                modelBoxes.Add(modelBox);
            }
            currentPanel.AutoScroll = true;


            panelDetails.AutoScroll = false;
            ShiftVisibilities(false);
            ShiftGroups(Height, Width);
            panelDetails.AutoScroll = true;
        }

        public void SetObjectiveTypes(QuestEntities questDetails)
        {
            if (string.IsNullOrEmpty(questDetails.enemyObjectiveType))
            {
                comboBox_eneObjType1.SelectedIndex = 0;
                comboBox_hosObjType.SelectedIndex = 0;
                comboBox_vehObjType.SelectedIndex = 0;
                comboBox_aniObjType.SelectedIndex = 0;
            }

            comboBox_eneObjType1.Text = questDetails.enemyObjectiveType;
            comboBox_hosObjType.Text = questDetails.hostageObjectiveType;
            comboBox_vehObjType.Text = questDetails.vehicleObjectiveType;
            comboBox_aniObjType.Text = questDetails.animalObjectiveType;
            comboBox_heliObjType.Text = "KILLREQUIRED";
            comboBox_WalkerObjType.Text = questDetails.walkerGearObjectiveType;
            comboBox_acItObjType.Text = questDetails.activeItemObjectiveType;
            comboBox_itObjType.Text = "RECOVERED";
        }

        public void SetEnemySubType(QuestEntities questDetails, int locId)
        {
            string[] subtypes = new string[0];
            comboBox_subtype.Items.Clear();
            comboBox_subtype2.Items.Clear();
            if (isAfgh(locId))
                subtypes = BodyInfo.afghSubTypes;
            else if (isMafr(locId))
                subtypes = BodyInfo.mafrSubTypes;

            comboBox_subtype.Items.AddRange(subtypes);
            comboBox_subtype2.Items.AddRange(subtypes);

            if (comboBox_subtype.Items.Contains(questDetails.soldierSubType))
                comboBox_subtype.Text = questDetails.soldierSubType;
            else if (comboBox_subtype.Items.Count > 0)
                comboBox_subtype.SelectedIndex = 0;
        }

        public void SetHostageBodies(QuestEntities questDetails, int locId)
        {
            comboBox_Body.Items.Clear();
            if (isMtbs(locId))
                foreach (BodyInfoEntry infoEntry in BodyInfo.BodyInfoArray)
                {
                    if (infoEntry.hasface)
                        this.comboBox_Body.Items.Add(infoEntry.bodyName);
                }
            else
                foreach (BodyInfoEntry infoEntry in BodyInfo.BodyInfoArray)
                {
                    this.comboBox_Body.Items.Add(infoEntry.bodyName);
                }

            if (comboBox_Body.Items.Count <= questDetails.hostageBodyIndex)
                comboBox_Body.SelectedIndex = 0;
            else
                comboBox_Body.SelectedIndex = questDetails.hostageBodyIndex;
        }

        internal void ShiftVisibilities(bool hideAll)
        {
            detailLists = new List<GroupBox>();
            Tuple<List<QuestBox>, GroupBox>[] detailTuples =
            {
                new Tuple<List<QuestBox>, GroupBox>(questEnemyBoxes, groupNewEneDet),
                new Tuple<List<QuestBox>, GroupBox>(CPEnemyBoxes, groupExistingEneDet),
                new Tuple<List<QuestBox>, GroupBox>(heliBoxes, groupHeliDet),
                new Tuple<List<QuestBox>, GroupBox>(walkerBoxes, groupWalkerDet),
                new Tuple<List<QuestBox>, GroupBox>(vehicleBoxes, groupVehDet),
                new Tuple<List<QuestBox>, GroupBox>(hostageBoxes, groupHosDet),
                new Tuple<List<QuestBox>, GroupBox>(animalBoxes, groupAnimalDet),
                new Tuple<List<QuestBox>, GroupBox>(itemBoxes, groupItemDet),
                new Tuple<List<QuestBox>, GroupBox>(activeItemBoxes, groupActiveItemDet),
                new Tuple<List<QuestBox>, GroupBox>(modelBoxes, groupStMdDet),
            };
            foreach (Tuple<List<QuestBox>, GroupBox> tuple in detailTuples)
            {
                if (tuple.Item1.Count > 0 && !hideAll)
                {
                    tuple.Item2.Visible = true;
                    detailLists.Add(tuple.Item2);
                }
                else tuple.Item2.Visible = false;
            }
        }

        internal void ShiftGroups(int height, int width)
        {
            Height = height; Width = width;
            dynamicPanelWidth = width / 5 + 30;
            int maxPanelWidth = 285;
            
            if (dynamicPanelWidth >= maxPanelWidth)
                dynamicPanelWidth = maxPanelWidth;

            if (detailLists.Count > 0)
                dynamicPanelWidth += (150 / detailLists.Count);
            
            foreach (GroupBox detailGroupBox in detailLists)
            {
                detailGroupBox.Width = dynamicPanelWidth;
            }

            int xOffset = 3 + originAnchor.Left;
            int bufferSpace = 4 + dynamicPanelWidth;
            
            for (int i = 0; i < detailLists.Count; i++)
            {
                detailLists[i].Left = xOffset + bufferSpace * i;
            }
        }

        public QuestEntities GetEntityLists()
        {

            List<Enemy> qenemies = new List<Enemy>();
            List<Enemy> cpenemies = new List<Enemy>();
            List<Hostage> hostages = new List<Hostage>();
            List<Vehicle> vehicles = new List<Vehicle>();
            List<Animal> animals = new List<Animal>();
            List<Item> items = new List<Item>();
            List<ActiveItem> activeItems = new List<ActiveItem>();
            List<Model> models = new List<Model>();
            List<Helicopter> helicopters = new List<Helicopter>();
            List<WalkerGear> walkers = new List<WalkerGear>();

            foreach (EnemyBox d in questEnemyBoxes)
                qenemies.Add(new Enemy(d, questEnemyBoxes.IndexOf(d)));

            foreach (EnemyBox d in CPEnemyBoxes)
                cpenemies.Add(new Enemy(d, CPEnemyBoxes.IndexOf(d)));

            foreach (HelicopterBox he in heliBoxes)
                helicopters.Add(new Helicopter(he));

            foreach (HostageBox d in hostageBoxes)
                hostages.Add(new Hostage(d, hostageBoxes.IndexOf(d)));

            foreach (WalkerGearBox w in walkerBoxes)
                walkers.Add(new WalkerGear(w, walkerBoxes.IndexOf(w)));

            foreach (VehicleBox d in vehicleBoxes)
                vehicles.Add(new Vehicle(d, vehicleBoxes.IndexOf(d)));

            foreach (AnimalBox d in animalBoxes)
                animals.Add(new Animal(d, animalBoxes.IndexOf(d)));

            foreach (ItemBox d in itemBoxes)
                items.Add(new Item(d, itemBoxes.IndexOf(d)));

            foreach (ActiveItemBox d in activeItemBoxes)
                activeItems.Add(new ActiveItem(d, activeItemBoxes.IndexOf(d)));

            foreach (ModelBox d in modelBoxes)
                models.Add(new Model(d, modelBoxes.IndexOf(d)));
            
            return new QuestEntities(qenemies, cpenemies, helicopters, hostages, walkers, vehicles, animals, items, activeItems, models, comboBox_Body.SelectedIndex, h_checkBox_intrgt.Checked, comboBox_subtype.Text, comboBox_eneObjType1.Text, comboBox_hosObjType.Text, comboBox_vehObjType.Text, comboBox_aniObjType.Text, comboBox_WalkerObjType.Text, comboBox_acItObjType.Text);
        }

        private void comboBox_Body_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshHostageLanguage();
            comboBox_hosObjType.Focus(); panelHosDet.Focus();
        }

        private void RefreshHostageLanguage()
        {
            if (comboBox_Body.Text.ToUpper().Contains("FEMALE"))
            {

                foreach (HostageBox hostageDetail in hostageBoxes)
                {
                    hostageDetail.h_comboBox_lang.Items.Clear();
                    hostageDetail.h_comboBox_lang.Items.Add("english");
                    hostageDetail.h_comboBox_lang.SelectedIndex = 0;
                }
            }
            else
            {
                foreach (HostageBox hostageDetail in hostageBoxes)
                {
                    int languageindex = hostageDetail.h_comboBox_lang.SelectedIndex;
                    hostageDetail.h_comboBox_lang.Items.Clear();
                    hostageDetail.h_comboBox_lang.Items.AddRange(new string[] { "english", "russian", "pashto", "kikongo", "afrikaans" });
                    hostageDetail.h_comboBox_lang.SelectedIndex = languageindex;
                }
            }

        }

        private void checkbox_spawnAll_Click(object sender, EventArgs e)
        {
            checkBox_spawnall.Checked = true;
            foreach (Control control in panelQuestEnemyDet.Controls.Find("e_checkBox_spawn", true))
            {
                CheckBox checkbox = (CheckBox)control;
                checkbox.Checked = true;
            }
            comboBox_subtype.Focus(); panelQuestEnemyDet.Focus();
        }

        private void checkbox_customizeAll_Click(object sender, EventArgs e)
        {
            checkBox_customizeall.Checked = true;
            foreach (Control control in panelCPEnemyDet.Controls.Find("e_checkBox_spawn", true))
            {
                CheckBox checkbox = (CheckBox)control;
                checkbox.Checked = true;
            }
            comboBox_subtype2.Enabled = true;
            comboBox_subtype2.Focus();
            comboBox_subtype2.Enabled = false;
            panelCPEnemyDet.Focus();
        }

        private void comboBox_subtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_subtype2.SelectedIndex = comboBox_subtype.SelectedIndex;
            panelQuestEnemyDet.Focus();
        }

        private void DetailFocus(object sender, EventArgs e)
        {
            ((Panel)sender).Focus();
        }

        private void comboBox_eneObjType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_eneObjType2.SelectedIndex = comboBox_eneObjType1.SelectedIndex;
            panelQuestEnemyDet.Focus();
        }
    }

    
}
