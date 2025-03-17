using SOC.Classes.Common;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.QuestObjects.Common;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Linq;
using SOC.Classes.Assets;
using SOC.Classes.Lua;
using SOC.Classes.QuestBuild.Assets;

namespace SOC.QuestObjects.Enemy
{
    public class EnemiesDetail : ObjectsDetail
    {
        public EnemiesDetail() { }

        public EnemiesDetail(List<Enemy> enemyList, EnemiesMetadata meta)
        {
            enemies = enemyList; enemyMetadata = meta;
        }

        [XmlElement]
        public EnemiesMetadata enemyMetadata { get; set; } = new EnemiesMetadata();

        [XmlArray]
        public List<Enemy> enemies { get; set; } = new List<Enemy>();

        static EnemyControl control = new EnemyControl();

        static EnemiesDetailVisualizer visualizer = new EnemiesDetailVisualizer(control);

        public override ObjectsMetadata GetMetadata()
        {
            return enemyMetadata;
        }

        public override ObjectsDetailVisualizer GetVisualizer()
        {
            return visualizer;
        }

        public override List<QuestObject> GetQuestObjects()
        {
            return enemies.Cast<QuestObject>().ToList();
        }

        public override void SetQuestObjects(List<QuestObject> qObjects)
        {
            enemies = qObjects.Cast<Enemy>().ToList();
        }

        public override void AddToDefinitionLua(DefinitionScriptBuilder definitionLua)
        {
            EnemyLua.GetDefinition(this, definitionLua);
        }

        public override void AddToMainLua(MainScriptBuilder mainLua)
        {
            EnemyLua.GetMain(this, mainLua);
        }

        public override void AddToAssets(CommonAssetsBuilder assetsBuilder)
        {
            EnemyAssets.GetEnemyAssets(this, assetsBuilder);
        }
    }
}
