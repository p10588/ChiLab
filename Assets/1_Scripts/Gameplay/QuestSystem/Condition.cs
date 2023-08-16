using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Chi.Gameplay.Quest
{
    public enum ConditionType
    {
        Item,
    }

    public interface ICondition {
        ConditionType ConditionType { get; }
        bool CheckCondition();
        void ExecuteCondition();
    }


    public class ItemCondition : ICondition {

        ConditionType ICondition.ConditionType => ConditionType.Item;

        public bool CheckCondition() {
            Debug.LogWarning("WARNING!! Dosent Implementation");
            return true;
        }

        public void ExecuteCondition() {
            Debug.LogWarning("WARNING!! Dosent Implementation");
        }
    }

    public static class ConditionFactory{

        private static Dictionary<ConditionType, Type> conditionByName = null;
        private static bool isInitalized => conditionByName != null;

        private static void InitalizeFactory() {
            var conditionType = Assembly.GetAssembly(typeof(ICondition)).GetTypes()
                               .Where(type => type.IsClass &&
                               typeof(ICondition).IsAssignableFrom(type));

            conditionByName = new Dictionary<ConditionType, Type>();

            foreach (var type in conditionType) {
                var tempInstance
                    = Activator.CreateInstance(type) as ICondition;
                conditionByName.Add(tempInstance.ConditionType, type);
            }

        }

        public static ICondition RequireCondition(ConditionType conditionType) {

            InitalizeFactory();

            if (conditionByName.TryGetValue(conditionType, out Type type)) {
                var questObjectProcessor = Activator.CreateInstance(type) as ICondition;
                return questObjectProcessor;
            } else {
                Debug.LogError("Cant find " + conditionType + " in Factory ");
            }

            return null;
        }
    }
}

