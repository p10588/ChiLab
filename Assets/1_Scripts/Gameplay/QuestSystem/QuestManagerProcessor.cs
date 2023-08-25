using System;
using System.Collections.Generic;
using System.Linq;
using Chi.Utilities.Extensions;
using Chi.Utilities.FileIO;
using UnityEngine;

namespace Chi.Gameplay.Quest
{

    public class QuestManagerProcessor {

        private IQuestManager _questManager;

        private object _userData;
        private QuestDataSheet _questDataSheet;
        private Dictionary<string, QuestData> rootQuestData;
        private Dictionary<string, List<IQuestTrigger>> _triggerGroupDic;

        private bool _isPrepared = false;
        private bool _isStarted = false;

        public QuestManagerProcessor(IQuestManager questManager) {
            this._questManager = questManager;
            InitalizeTriggerGroup();
        }

        private void InitalizeTriggerGroup() {
            this._triggerGroupDic = new Dictionary<string, List<IQuestTrigger>>();

            for (int i = 0; i < _questManager.triggerGroupRoot.Count; i++) {
                if (TryGetTriggersInChild(_questManager.triggerGroupRoot[i], out List<IQuestTrigger> triggerGroup)) {
                    this._triggerGroupDic.Add(_questManager.triggerGroupRoot[i].name, triggerGroup);
                }
            }
        }

        private bool TryGetTriggersInChild(GameObject triggerGroupRoot, out List<IQuestTrigger> triggers) {
            triggers = new List<IQuestTrigger>();
            triggerGroupRoot.GetComponentsInChildren<IQuestTrigger>(triggers);
            if (triggers.IsNullorEmpty()) {
                Debug.LogError(triggerGroupRoot.name + " doesnt have any child game object");
                return false;
            }

            return true;
        }

        public void PrepareQuest() {
            TryProcessData(this._questManager.questDataSource);
        }

        private void TryProcessData(string data) {
            if (this._isPrepared) {
                Debug.Log("Quest has already prepared!!!!!");
                return;
            }

            try {
                QuestDataSheetHandler dataSheetHandler = new QuestDataSheetHandler();
                this._questDataSheet = dataSheetHandler.GetQuestDataSheet(data);
                this._userData = GetUserData();
            } catch (Exception e) {
                Debug.LogError(e);
            }

            this._isPrepared = true;
        }


        private object GetUserData() {
            Debug.LogWarning("Get UserData is not Implemented");
            return null;
        }


        public void TryStartQuest() {
            if (!this._isPrepared) {
                Debug.LogError("Quest didnt Prepared!!!!");
                return;
            }

            if (this._isStarted) {
                Debug.LogError("Quest has already started!!!!!");
                return;
            }

            try {
                QuestSatrtProcess(this._questDataSheet, this._userData);
            }catch(Exception e) {
                Debug.LogError(e);
            }

            this._isStarted = true;
        }

        private void QuestSatrtProcess(QuestDataSheet questDataSheet, object userData) {
            if (questDataSheet == null)
                throw new ArgumentNullException("Quest Data Sheet is Null");

            if (questDataSheet.QuestDatas.IsNullorEmpty())
                throw new ArgumentNullException("QuestDataSheet.QuestDatas is Null or Empty");

            if (userData == null) FirstStartQuest();
            else StartQuestFromUserData(userData);

        }

        private void FirstStartQuest() {
            foreach (KeyValuePair<string, QuestData> data in this._questDataSheet.QuestDatas) {
                ActiveRootQuest(data.Value);
            }
        }

        private void ActiveRootQuest(QuestData questData) {

            IQuestTrigger trigger = TryGetTrigger(questData.QuestGroup, questData.Index);

            if (trigger == null) {
                Debug.LogError("Trigger is Null");
                return;
            }

            if (!this.TryCheckCondition(questData) || !this.CheckIsRootData(questData.Id))
                return;

            IQuestProcessor questProcessor = this.CreateQuest(trigger, questData);
            questProcessor.ChangeProc(QuestProcState.Active);
        }

        private bool TryCheckCondition(QuestData questData) {
            try {
                if (questData.ActiveCondition == null) return true;

                return questData.ActiveCondition.CheckCondition();

            } catch (Exception e) {
                Debug.LogError(e);
                return false;
            }
        }

        private bool CheckIsRootData(string questId) {
            if (questId.Contains("*")) return true;
            return false;
        }

        private IQuestTrigger TryGetTrigger(string questGroup, int questIndex) {
            try {
                return GetTrigger(questGroup, questIndex);
            }catch(Exception e) {
                Debug.LogError(e);
                return null;
            }
        }

        private IQuestTrigger GetTrigger(string questGroup, int questIndex) {

            if (this._triggerGroupDic.IsNullorEmpty())
                throw new ArgumentNullException("Trigger group data is null or empty");

            if( this._triggerGroupDic.HaveNullValues())
                throw new ArgumentNullException("Trigger group data has null value");

            if (this._triggerGroupDic.TryGetValue(questGroup, out List<IQuestTrigger> groups)) {
                return GetTriggerFromTriggerGroups(groups, questIndex);
            }

            throw new ArgumentException($"Can't get {questGroup} in Trigger Group Dic");

        }

        private IQuestTrigger GetTriggerFromTriggerGroups(List<IQuestTrigger> group, int index) {
            if(index >= group.Count)
                throw new ArgumentOutOfRangeException(
                    "Trigger group didnt have enough Quest Trigger for Quest Index"
                );
            return group[index];
        }

        private void StartQuestFromUserData(object userData) {
            throw new System.NotImplementedException();
        }


        public void PrepareAndStartQuest() {
            TryProcessData(this._questManager.questDataSource);
            TryStartQuest();
        }


        public IQuestProcessor SpawnQuestObject(IQuestTrigger trigger, QuestData data) {
            if (!CheckPrepareAndStart()) return null;

            return CreateQuest(trigger, data);
        }

        public IQuestProcessor SpawnQuestObject(IQuestTrigger trigger, string questId) {
            if (!CheckPrepareAndStart()) return null;

            QuestData data = GetQuestData(questId);
            return CreateQuest(trigger, data);
        }

        public IQuestProcessor SpawnQuestObject(string questId) {
            if (!CheckPrepareAndStart()) return null;

            QuestData data = TryGetQuestData(questId);
            IQuestTrigger trigger = TryGetTrigger(data.QuestGroup, data.Index);
            return CreateQuest(trigger, data);
        }

        private QuestData TryGetQuestData(string questId) {
            try {
                return GetQuestData(questId);
            } catch(Exception e) {
                Debug.LogError(e);
                return null;
            }
        }

        private QuestData GetQuestData(string questId) {
            if (this._questDataSheet.QuestDatas.TryGetValue(questId, out QuestData data)) {
                return data;
            }
            throw new ArgumentException($"Cant found {questId} in Data Sheet");
        }

        private IQuestProcessor CreateQuest(IQuestTrigger trigger, QuestData questData) {
            IQuestProcessor questProcessor
                = QuestProcessorFactory.RequireQuestProcessor(questData.QuestType, questData, this);
            trigger.InitalizeTrigger(questProcessor);
            return questProcessor;
        }

        public void RemoveQuest(IQuestTrigger trigger) {
            if (!CheckPrepareAndStart()) return;
            trigger.UninitalizeTrigger();
        }

        private bool CheckPrepareAndStart() {
            if (!this._isStarted || !this._isPrepared) {
                Debug.LogError("Manager didn't prepared or started");
                return false;
            }
            return true;
        }

    }

    

}

