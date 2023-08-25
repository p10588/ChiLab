using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chi.Gameplay.Items
{

    public interface IItem
    {
        GameObject GameObject { get; }
        string itemAsset { get; }
    }

    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] string ItemAsset;
        [SerializeField] bool ActiveOnStart;

        GameObject IItem.GameObject => this.gameObject;
        string IItem.itemAsset => this.ItemAsset;

        private IItemTrigger _trigger;
        private IItemProcessor _processor;

        private void Awake() {
            this._trigger = GetComponent<IItemTrigger>();
            this._processor
                = ItemProcessorFactory.RequireItemProcressor(ItemType.Normal, this);
        }

        private void Start() {       
            this.InitalizeTrigger();
            this.InitalizeProcessor();
        }

        private void InitalizeProcessor() {
            if (ActiveOnStart) this._processor.ChangeProc(ItemStateType.Active);
        }

        private void InitalizeTrigger() {
            Action enterAction = () => { this._processor.ChangeProc(ItemStateType.Interact); };
            Action exitAction = () => { this._processor.ChangeProc(ItemStateType.Active); };
            this._trigger.InitalizeTrigger(enterAction, exitAction);
        }

        private void OnDestroy() {
            this._trigger.UninitalizeTrigger();
        }
    }

}