using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chi.Utilities.Core;
using UnityEngine;

namespace Chi.Gameplay.Triggers {

    public static class TriggerProcessorFactory
    {
        private static Dictionary<TriggerProcessorType, Type> TriggerProcessorByName = null;
        private static bool isInitalized => TriggerProcessorByName != null;

        private static void InitalizeFactory() {
            var triggerType
                = Assembly.GetAssembly(typeof(ITriggerProcessor)).GetTypes()
                  .Where(type => type.IsClass && typeof(ITriggerProcessor).IsAssignableFrom(type));

            TriggerProcessorByName = new Dictionary<TriggerProcessorType, Type>();

            foreach (var type in triggerType) {
                var tempInstance
                    = Activator.CreateInstance(type, (ITriggerProcessor)null) as ITriggerProcessor;
                TriggerProcessorByName.Add(tempInstance.TriggerProcessorType, type);
            }

        }

        public static ITriggerProcessor RequireTriggerProcressor(TriggerProcessorType triggerType,
                                                                 ITrigger trigger) {

            InitalizeFactory();

            if (TriggerProcessorByName.TryGetValue(triggerType, out Type type)) {
                var triggerProcessor = Activator.CreateInstance(type, trigger) as ITriggerProcessor;
                return triggerProcessor;
            } else {
                Debug.LogError("Cant find " + triggerType + " in Factory ");
            }

            return null;
        }
    }


}
