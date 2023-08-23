using System;
using Chi.Gameplay.Quest;
using Chi.Gameplay.Triggers;
using NaughtyAttributes;
using UnityEngine;

namespace Chi.Gameplay.Triggers
{

    [RequireComponent(typeof(SphereCollider))]
    public class QuestTrigger : MonoBehaviour, ITrigger, IQuestTrigger { 
        [SerializeField][Tag] string[] Tags;

        string[] ITrigger.tags => this.Tags;
        GameObject ITrigger.gameObject => this.gameObject;

        IQuestProcessor IQuestTrigger.questProcessor
                            => this._triggerProcessor?.GetProcessor<IQuestProcessor>();

        private ITriggerProcessor _triggerProcessor;

        private void Start() {
            this.GetComponent<Collider>().isTrigger = true;
            GetTriggerProcessor();
        }

        private void OnTriggerEnter(Collider other) {
            this._triggerProcessor?.TriggerEnterProcess(other);
        }

        private void OnTriggerExit(Collider other) {
            this._triggerProcessor?.TriggerExitProcess(other);
        }

        private void GetTriggerProcessor() {
            if (this._triggerProcessor != null) return;
            this._triggerProcessor
                = TriggerProcessorFactory.RequireTriggerProcressor(TriggerProcessorType.Quest, this);
        }

        void IQuestTrigger.InitalizeTrigger(IQuestProcessor questProcessor) {
            GetTriggerProcessor();
            this._triggerProcessor.InitalizeTriggerProcessor<IQuestProcessor>(questProcessor);
        }

        void IQuestTrigger.UninitalizeTrigger() {
            this._triggerProcessor.UninitalizeTriggerProcessor();
            this._triggerProcessor = null;
        }
    }
}

