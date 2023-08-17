using System.Collections.Generic;

namespace Chi.Gameplay.Quest
{

    public class QuestData
    {
        public string Id;
        public int Index;
        public bool IsRoot;
        public string QuestGroup;
        public string QuestName;
        public QuestType QuestType;
        public string NextQuestId;
        public bool EnableRetrigger;
        public string InteractText;
        public ICondition ActiveCondition;
        public string QuestHint;
        public string ShowInProgressUI;
        public ICondition PassCondition;
        public IReward PassReward;
    }

    public class QuestDataSheet
    {
        public Dictionary<string, QuestData> QuestDatas;
    }


}
