using System;
using System.Collections.Generic;
using System.Linq;
using Chi.Gameplay.Items;
using Chi.Gameplay.Quest;
using Chi.Utilities.Extensions;
using UnityEngine;

namespace Chi.Gameplay.Triggers
{

    public class ItemTriggerProcesser : ITriggerProcessor
    {
        private ITrigger _trigger;
        private List<GameObject> _triggerObj;
        private Action _enterAction;
        private Action _exitAction;
        private bool _isInitalize = false;

        TriggerProcessorType ITriggerProcessor.TriggerProcessorType => TriggerProcessorType.Item;
        bool ITriggerProcessor.IsInitalized => this._isInitalize;

        public ItemTriggerProcesser(ITrigger trigger) {
            this._trigger = trigger;
            this._triggerObj = new List<GameObject>();
        }

        void ITriggerProcessor.TriggerEnterProcess(Collider other) {
            if (!IsValidTag(other.tag) || HasTriggered(other.gameObject)) return;

            this._enterAction?.Invoke();

            this._triggerObj.Add(other.gameObject);
        }

        void ITriggerProcessor.TriggerExitProcess(Collider other) {
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

        void ITriggerProcessor.InitalizeTriggerProcessor<T>(T processor) {
            throw new NotImplementedException();
        }

        void ITriggerProcessor.InitalizeTriggerAction(Action enterAction, Action exitAction) {
            if (this._isInitalize) return;
            this._enterAction = enterAction;
            this._exitAction = exitAction;
            this._isInitalize = true;
        }

        void ITriggerProcessor.UninitalizeTriggerProcessor() {
            if (!this._isInitalize) return;
            this._enterAction = null;
            this._exitAction = null;
            this._isInitalize = false;
        }

        T ITriggerProcessor.GetProcessor<T>() {
            throw new NotImplementedException();
        }
    }

}

