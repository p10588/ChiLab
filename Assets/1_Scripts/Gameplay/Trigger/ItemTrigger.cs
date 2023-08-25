using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using Chi.Utilities.Extensions;
using Chi.Gameplay.Items;

namespace Chi.Gameplay.Triggers
{

    [RequireComponent(typeof(SphereCollider), typeof(Item))]
    public class ItemTrigger : MonoBehaviour, ITrigger, IItemTrigger
    {
        [SerializeField][Tag] string[] Tags;

        public ITriggerProcessor triggerProcessor { get; private set; }

        string[] ITrigger.tags => this.Tags;

        private void Awake() {
            this.GetComponent<Collider>().isTrigger = true;
            this.triggerProcessor
                = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Item, this);
        }

        private void OnTriggerEnter(Collider other) {
            this.triggerProcessor.TriggerEnterProcess(other);
        }

        private void OnTriggerExit(Collider other) {
            this.triggerProcessor.TriggerExitProcess(other);
        }


        void IItemTrigger.InitalizeTrigger(Action enterAction, Action exitAction) {
            this.triggerProcessor.InitalizeTriggerAction(enterAction, exitAction);
        }

        void IItemTrigger.UninitalizeTrigger() {
            this.triggerProcessor.UninitalizeTriggerProcessor();
        }
    }


    

    
}

