using System;
using System.Collections.Generic;
using System.Linq;
using Chi.Gameplay.Quest;
using Chi.Utilities.Extensions;
using UnityEngine;

namespace Chi.Gameplay.Triggers
{

    public class QuestTriggerProcesser : ITriggerProcessor
    {
        private ITrigger _trigger;
        private List<GameObject> _triggerObj;
        private Action _enterAction;
        private Action _exitAction;
        private IQuestProcessor _questProcessor;
        private bool _isInitalize => this._questProcessor != null;

        TriggerProcessorType ITriggerProcessor.TriggerProcessorType => TriggerProcessorType.Quest;

        bool ITriggerProcessor.IsInitalized => this._isInitalize;

        public QuestTriggerProcesser(ITrigger trigger) {
            this._trigger = trigger;
            this._triggerObj = new List<GameObject>();
            
        }

        void ITriggerProcessor.TriggerEnterProcess(Collider other) {
            if (!_isInitalize) return;

            if (!IsValidTag(other.tag) || HasTriggered(other.gameObject)) return;

            this._enterAction?.Invoke();

            this._triggerObj.Add(other.gameObject);
        }

        void ITriggerProcessor.TriggerExitProcess(Collider other) {
            if (!_isInitalize) return;

            if (!IsValidTag(other.tag) || !HasTriggered(other.gameObject)) return;

            this._exitAction?.Invoke();

            this._triggerObj.Remove(other.gameObject);
        }


        private bool IsValidTag(string tag) {
            if (this._trigger.tags.IsNullorEmpty()) return false;
            if (this._trigger.tags.Contains(tag)) return true;
            return false;
        }

        private bool HasTriggered(GameObject triggerObject) {
            if (this._triggerObj.IsNullorEmpty()) return false;
            if (this._triggerObj.Contains(triggerObject)) return true;
            return false;
        }

        void ITriggerProcessor.InitalizeTriggerProcessor<T>(T questProcessor) {
            this._questProcessor = (IQuestProcessor)questProcessor;
            this._enterAction = () => { this._questProcessor.ChangeProc(QuestProcState.InProgress); };
            this._exitAction = () => { this._questProcessor.ChangeProc(QuestProcState.Active); };
        }

        void ITriggerProcessor.UninitalizeTriggerProcessor() {
            this._questProcessor = null;
            this._enterAction = null;
            this._exitAction = null;
        }

        T ITriggerProcessor.GetProcessor<T>() {
            return (T)this._questProcessor;
        }

        void ITriggerProcessor.InitalizeTriggerAction(Action enterAction, Action exitAction) {
            throw new NotImplementedException();
        }
    }

}

