using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using Chi.Utilities.Extensions;

namespace Chi.Gameplay.Triggers
{
    
    public interface ITrigger
    {
        string[] tags { get; }
        GameObject gameObject { get; }
    }

    [RequireComponent(typeof(SphereCollider))]
    public class Trigger : MonoBehaviour, ITrigger
    {
        [SerializeField][Tag] string[] Tags;

        private ITriggerProcessor _triggerProcessor;

        string[] ITrigger.tags => this.Tags;
        GameObject ITrigger.gameObject => this.gameObject;
        
        private void Start() {
            this.GetComponent<Collider>().isTrigger = true;
            InitalizeProcessor();
        }

        private void OnTriggerEnter(Collider other) {
            this._triggerProcessor.TriggerEnterProcess(other);
        }

        private void OnTriggerExit(Collider other) {
            this._triggerProcessor.TriggerExitProcess(other);
        }

        private void InitalizeProcessor() {
            this._triggerProcessor = GetProcessorFromFactory(TriggerProcessorType.Normal);
        }

        private ITriggerProcessor GetProcessorFromFactory(TriggerProcessorType processorType) {
            return TriggerProcessorFactory.RequireTriggerProcressor(processorType, this);
        }

    }


    

    
}

