using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Chi.Gameplay.Quest
{
    public static class QuestProcessorFactory {

        private static Dictionary<QuestType, Type> questProcessorByName = null;
        private static bool isInitalized =false;


        private static void InitalizeFactory() {
            if (isInitalized) return;

            var questObjectType
                = Assembly.GetAssembly(typeof(IQuestProcessor)).GetTypes()
                  .Where(type => type.IsClass && typeof(IQuestProcessor).IsAssignableFrom(type));

            questProcessorByName = new Dictionary<QuestType, Type>();

            foreach (var type in questObjectType) {
                var tempInstance
                    = Activator.CreateInstance(type, (QuestData)null , (QuestManagerProcessor)null) as IQuestProcessor;
                questProcessorByName.Add(tempInstance.QuestProcessorType, type);
            }
            isInitalized = true;
        }

        public static IQuestProcessor RequireQuestProcessor(QuestType questProcessorType,
                                                            QuestData questData,
                                                            QuestManagerProcessor questMgrProcessor) {

            InitalizeFactory();

            if(questProcessorByName.TryGetValue(questProcessorType, out Type type)) {
                var questObjectProcessor
                    = Activator.CreateInstance(type, questData, questMgrProcessor) as IQuestProcessor;
                return questObjectProcessor;
            } else {
                Debug.LogError("Cant find " + questProcessorType + " in Factory ");
            }

            return null;
        }
    }
}

