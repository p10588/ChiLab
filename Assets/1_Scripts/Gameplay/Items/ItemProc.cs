using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Chi.Gameplay.Items
{
    public enum ItemStateType
    {
        NonActive,
        Active,
        Interact,
        Deactive,
        Destroy,
    }

    public interface IItemProc
    {
        void Entry();
        void Leave();
    }
    public class ItemActiveProc : IItemProc
    {
        private IItemProcessor _processor;
        private IItemAssetBuilder _assetBuilder;

        public ItemActiveProc(IItemProcessor processor) {
            this._processor = processor;
            
        }

        void IItemProc.Entry() {
            Debug.Log("Item Active Entry");

            BuildAsset();

            Action callback = ()
                => { this._processor.ChangeProc(ItemStateType.Deactive); };
            StartLifeTime(callback);
        }

        void IItemProc.Leave() {
            Debug.Log("Item Active Leave");
        }

        private void BuildAsset() {
            string id = this._processor.AssetId;

            if (string.IsNullOrEmpty(id)) return;
            this._assetBuilder?.CreateAsset(id);
        }

        async void StartLifeTime(Action delayCallback) {
            float lifeTime = this._processor.LifeTime;
            if ( lifeTime <= 0) return;

            int delayTime = (int)(lifeTime * 1000);

            await Task.Delay(delayTime);

            delayCallback?.Invoke();

        }
    }
    public class ItemInteractProc : IItemProc
    {
        private IItemProcessor _processor;

        public ItemInteractProc(IItemProcessor processor){
            this._processor = processor;
        }

        void IItemProc.Entry() {
            Debug.Log("Item Interact Entry");
        }

        void IItemProc.Leave() {
            Debug.Log("Item Interact Leave");
        }
    }


    public class ItemDeactiveProc : IItemProc
    {
        private IItemProcessor _processor;

        public ItemDeactiveProc(IItemProcessor processor) {
            this._processor = processor;
        }

        void IItemProc.Entry() {
            Debug.Log("Item Deactive Entry");
        }

        void IItemProc.Leave() {
            Debug.Log("Item Deactive Leave");
        }
    }

    public class ItemDestroyProc : IItemProc
    {
        private IItemProcessor _processor;

        public ItemDestroyProc(IItemProcessor processor) {
            this._processor = processor;
        }

        void IItemProc.Entry() {
            Debug.Log("Item Destroy Entry");
            
        }

        void IItemProc.Leave() {
            
        }
    }
}

