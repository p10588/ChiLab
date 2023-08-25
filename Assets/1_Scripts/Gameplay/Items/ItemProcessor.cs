using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Chi.Gameplay.Items
{
    public enum ItemType {
        Normal,
    }

    public interface IItemTrigger
    {
        void InitalizeTrigger(Action enterAction, Action exitAction);
        void UninitalizeTrigger();
    }

    public interface IItemAssetBuilder
    {
        GameObject CreateAsset(string assetId);
    }

    public interface IItemProcessor
    {
        float LifeTime { get; }
        string AssetId { get; }
        ItemType ItemType { get; }
        ItemStateType CurItemState { get;}
        void ChangeProc(ItemStateType state);
        void Deactive();
        void Destroy();
    }

    public class ItemProcessor : IItemProcessor
    {
        ItemType IItemProcessor.ItemType => ItemType.Normal;
        ItemStateType IItemProcessor.CurItemState => this._curItemState;

        float IItemProcessor.LifeTime => this._lifeTime;
        string IItemProcessor.AssetId => this._assetId;

        private IItem _item;
        private IItemTrigger _trigger;
        private float _lifeTime;
        private string _assetId;

        private ItemStateType _curItemState;
        private IItemProc _curQuestProc;
        private Dictionary<ItemStateType, IItemProc> _dicQuestProc;
        
        public ItemProcessor(IItem item) {
            this._item = item;
            InitalDicItemProc();
        }

        private void InitalDicItemProc() {
            if (this._dicQuestProc == null) {
                this._dicQuestProc = new Dictionary<ItemStateType, IItemProc>();
                this._dicQuestProc.Add(ItemStateType.NonActive, null);
                this._dicQuestProc.Add(ItemStateType.Active, new ItemActiveProc(this));
                this._dicQuestProc.Add(ItemStateType.Interact, new ItemInteractProc(this));
                this._dicQuestProc.Add(ItemStateType.Deactive, new ItemDeactiveProc(this));
                this._dicQuestProc.Add(ItemStateType.Destroy, new ItemDestroyProc(this));
            }
        }

        #region IQuestObjectProcessor Implementation

        void IItemProcessor.ChangeProc(ItemStateType state) {
            this.SwitchItemProc(state);
        }

        private void SwitchItemProc(ItemStateType state) {
            if (this._dicQuestProc.TryGetValue(state, out IItemProc itemProc)) {
                this._curQuestProc?.Leave();

                this._curItemState = state;
                this._curQuestProc = itemProc;
                this._curQuestProc?.Entry();
            }
        }

        void IItemProcessor.Deactive() {
            throw new NotImplementedException();
        }

        void IItemProcessor.Destroy() {
            throw new NotImplementedException();
        }

        #endregion





    }


}


