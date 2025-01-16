using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Fox2;

namespace SOC.QuestObjects.Model
{
    static class ModelFox2
    {
        internal static void AddQuestEntities(ModelsDetail questDetail, DataSet dataSet, List<Fox2EntityClass> entityList)
        {
            List<Model> models = questDetail.models;

            if (models.Count() > 0)
            {
                foreach(Model model in models)
                {
                    StaticModel staticModel = new StaticModel(model.GetObjectName(), dataSet, model.model, model.collision);
                    Transform modelTransform = new Transform(staticModel, model.position);

                    staticModel.SetParameter(modelTransform);

                    entityList.Add(staticModel);
                    entityList.Add(modelTransform);
                }
            }
        }
    }
}
