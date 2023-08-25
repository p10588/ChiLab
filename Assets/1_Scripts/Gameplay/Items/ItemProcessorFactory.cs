using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Chi.Gameplay.Items
{

    public static class ItemProcessorFactory
    {
        private static Dictionary<ItemType, Type> ItemByName = null;
        private static bool isInitalized => ItemByName != null;

        private static void InitalizeFactory() {
            var itemType
                = Assembly.GetAssembly(typeof(IItemProcessor)).GetTypes()
                  .Where(type => type.IsClass && typeof(IItemProcessor).IsAssignableFrom(type));

            ItemByName = new Dictionary<ItemType, Type>();

            foreach (var type in itemType) {
                var tempInstance
                    = Activator.CreateInstance(type, (IItem)null) as IItemProcessor;
                ItemByName.Add(tempInstance.ItemType, type);
            }

        }

        public static IItemProcessor RequireItemProcressor(ItemType itemType, IItem item) {

            InitalizeFactory();

            if (ItemByName.TryGetValue(itemType, out Type type)) {
                var itemProcessor = Activator.CreateInstance(type, item) as IItemProcessor;
                return itemProcessor;
            } else {
                Debug.LogError("Cant find " + itemType + " in Factory ");
            }

            return null;
        }
    }
}
