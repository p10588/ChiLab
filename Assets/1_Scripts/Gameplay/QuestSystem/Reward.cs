using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace Chi.Gameplay.Quest
{
    public enum RewardType
    {
        Item,
    }

    public interface IReward
    {
        RewardType RewardType { get; }
        bool GetReward();
    }

    public class ItemReward : IReward
    {
        RewardType IReward.RewardType => RewardType.Item;

        public bool GetReward() {
            //Do Get Reward
            Debug.LogWarning("WARNING!! Dosent Implementation");
            return true;
        }
    }

    public class RewardFactory
    {
        private static Dictionary<RewardType, Type> RewardByName = null;
        private static bool isInitalized => RewardByName != null;

        private static void InitalizeFactory() {
            var conditionType
                = Assembly.GetAssembly(typeof(IReward)).GetTypes()
                  .Where(type => type.IsClass && typeof(IReward).IsAssignableFrom(type));

            RewardByName = new Dictionary<RewardType, Type>();

            foreach (var type in conditionType) {
                var tempInstance
                    = Activator.CreateInstance(type) as IReward;
                RewardByName.Add(tempInstance.RewardType, type);
            }

        }

        public static IReward RequireReward(RewardType rewardType) {

            InitalizeFactory();

            if (RewardByName.TryGetValue(rewardType, out Type type)) {
                var reward = Activator.CreateInstance(type) as IReward;
                return reward;
            } else {
                Debug.LogError("Cant find " + rewardType + " in Factory ");
            }

            return null;
        }
    }
}


