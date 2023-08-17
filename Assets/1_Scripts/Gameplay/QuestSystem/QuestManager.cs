using UnityEngine;
using System.Collections.Generic;
using Chi.Utilities.Extensions;

namespace Chi.Gameplay.Quest {

    public interface IQuestManager
    {
        string questDataSource { get; }
        List<GameObject> triggerGroupRoot { get; }
    }

    public class QuestManager : MonoBehaviour, IQuestManager {
        [SerializeField]
        private TextAsset QuestDataSource;
        [SerializeField]
        private List<GameObject> TriggerGroupRoot;

        private QuestManagerProcessor _processor;

        string IQuestManager.questDataSource => QuestDataSource.text;

        List<GameObject> IQuestManager.triggerGroupRoot => TriggerGroupRoot;

        private void Awake() {  
            this._processor = new QuestManagerProcessor(this);
        }

        private void Start() {
            this._processor.PrepareAndStartQuest();

        }

    }

    

}
