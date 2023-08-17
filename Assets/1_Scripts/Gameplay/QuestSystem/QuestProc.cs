using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chi.Gameplay.Quest
{
    public interface IQuestProc
    {
        IQuestProcessor QuestProcessor { get; }
        void Entry();
        void Leave();
    }

    public class ActiveProc : IQuestProc
    {
        IQuestProcessor IQuestProc.QuestProcessor => _questProcessor;

        IQuestProcessor _questProcessor;

        private QuestData _questData => this._questProcessor.QuestData;

        public ActiveProc(IQuestProcessor questProcessor) {
            this._questProcessor = questProcessor;
        }

        public void Entry() {
            Debug.Log(this._questData.QuestName + " is Active Entry");
        }

        public void Leave() {
            Debug.Log(this._questData.QuestName + " is Active Leave");
        }
    }

    public class InProgressProc : IQuestProc
    {
        IQuestProcessor IQuestProc.QuestProcessor => _questProcessor;

        IQuestProcessor _questProcessor;

        private QuestData _questData => this._questProcessor.QuestData;

        public InProgressProc(IQuestProcessor questProcessor) {
            this._questProcessor = questProcessor;
        }

        public void Entry() {
            Debug.Log(this._questData.QuestName + " is InProgress Entry");
        }

        public void Leave() {
            Debug.Log(this._questData.QuestName + " is InProgress Leave");
        }
    }
    public class ResolveProc : IQuestProc
    {
        IQuestProcessor IQuestProc.QuestProcessor => _questProcessor;

        IQuestProcessor _questProcessor;

        private QuestData _questData => this._questProcessor.QuestData;
        private QuestManagerProcessor _mgrProcessor
            => this._questProcessor.QuestManagerProcessor;

        public ResolveProc(IQuestProcessor questProcessor) {
            this._questProcessor = questProcessor;
        }

        public void Entry() {
            Debug.Log(this._questData.QuestName + " is Resolve Entry");
            this.TryCreateAndActiveNextQuest();
        }

        public void Leave() {
            Debug.Log(this._questData.QuestName + " is Resolve Leave");

        }

        private void TryCreateAndActiveNextQuest() {
            try {
                CreateAndActiveNextQuest();
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }

        private void CreateAndActiveNextQuest() {
            if (this._questData == null) 
                throw new ArgumentNullException("Quest data is null");

            if(this._mgrProcessor == null)
                throw new ArgumentNullException("Quest Manager Processor is null");

            if (string.IsNullOrEmpty(this._questData.NextQuestId)) return;

            string[] nextQuestIds = this._questData.NextQuestId.Split(',');

            for(int i=0; i < nextQuestIds.Length; i++) {
                IQuestProcessor questProcessor = this._mgrProcessor.SpawnQuestObject(nextQuestIds[i]);
                questProcessor.ChangeProc(QuestProcState.Active);
            }

        }
    }
}
