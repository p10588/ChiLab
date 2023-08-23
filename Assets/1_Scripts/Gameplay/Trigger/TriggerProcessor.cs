using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chi.Gameplay.Quest;
using Chi.Utilities.Extensions;
using UnityEngine;

namespace Chi.Gameplay.Triggers
{
    public enum TriggerProcessorType
    {
        Normal,
        Quest,
        Item,
    }

    public interface ITriggerProcessor
    {
        bool IsInitalized { get; }
        TriggerProcessorType TriggerProcessorType { get; }
        void InitalizeTriggerProcessor<T>(T processor);
        void InitalizeTriggerAction(Action enterAction, Action exitAction);
        void UninitalizeTriggerProcessor();
        void TriggerEnterProcess(Collider other);
        void TriggerExitProcess(Collider other);
        T GetProcessor<T>();
    }

    public class TriggerProcesser : ITriggerProcessor
    {
        private ITrigger _trigger;
        private List<GameObject> _triggerObj;
        private Action _enterAction;
        private Action _exitAction;
        private bool _isInitalized;

        TriggerProcessorType ITriggerProcessor.TriggerProcessorType => TriggerProcessorType.Normal;
        bool ITriggerProcessor.IsInitalized => this._isInitalized;


        public TriggerProcesser(ITrigger trigger) {
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
            if (this._isInitalized) {
                Debug.LogWarning("Trigger processor has been initalized");
                return;
            }
            throw new NotImplementedException("Trigger processor isn't implemented");
        }

        void ITriggerProcessor.InitalizeTriggerAction(Action enterAction, Action exitAction) {

            if (this._isInitalized) {
                Debug.LogWarning("Trigger processor has been initalized");
                return;
            }

            this._enterAction = enterAction;
            this._exitAction = exitAction;
            this._isInitalized = true;
        }

        void ITriggerProcessor.UninitalizeTriggerProcessor() {
            this._enterAction = null;
            this._exitAction = null;
            this._isInitalized = false;
        }

        T ITriggerProcessor.GetProcessor<T>() {
            throw new NotImplementedException();
        }

        
    }

}

