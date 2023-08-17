using System;
using System.Collections.Generic;
using Chi.Utilities.FileIO;
using UnityEngine;

namespace Chi.Gameplay.Quest
{

    public class QuestDataSheetHandler
    {
        public QuestDataSheetHandler() { }

        public QuestDataSheet GetQuestDataSheet(string data) {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("Quest data sheet asset is null");
            return ConvertToQuestDataSheet(data); ;
        }

        private QuestDataSheet ConvertToQuestDataSheet(string data) {
            QuestDataSheet dataSheet = new QuestDataSheet();
            dataSheet.QuestDatas = new Dictionary<string, QuestData>();

            List<Dictionary<string, object>> csvRawData = CSVReader.ReadByRawText(data);

            for (int i = 0; i < csvRawData.Count; i++) {
                QuestData questData = ConvertToQuestData(csvRawData[i]);
                if (questData != null)
                    dataSheet.QuestDatas.Add(questData.Id, questData);
            }

            return dataSheet;
        }

        private QuestData ConvertToQuestData(Dictionary<string, object> data) {
            QuestData questData = null;

            if (data["#"] is int) {
                questData = new QuestData();
                questData.Index = ((int)data["#"]) - 1;
                questData.IsRoot = (int)data["IsRoot"] == 1;
                questData.QuestName = (string)data["QuestName"];
                questData.Id = RootPrefix(questData.IsRoot)
                               + (int)data["#"] + "_"
                               + questData.QuestName;
                questData.QuestGroup = (string)data["QuestGroup"];
                questData.QuestType = (QuestType)Enum.Parse(typeof(QuestType), (string)data["QuestType"]);
                questData.NextQuestId = (string)data["NextQuestId"];
                questData.ActiveCondition = null;// ConditionFactory.RequireCondition(ConditionType.Item);
                questData.EnableRetrigger = (int)data["Retriggerable"] == 1;
                questData.InteractText = (string)data["InteractText"];
                questData.QuestHint = (string)data["QuestHint"];
                questData.ShowInProgressUI = (string)data["ShowInProgressUI"];
                questData.PassCondition = ConditionFactory.RequireCondition(ConditionType.Item);
                questData.PassReward = RewardFactory.RequireReward(RewardType.Item);
                Debug.Log("Finish " + questData.Id);
            }

            return questData;
        }

        private string RootPrefix(bool isRoot) {
            if (isRoot) return "*";
            else return string.Empty;
        }

    }
}
