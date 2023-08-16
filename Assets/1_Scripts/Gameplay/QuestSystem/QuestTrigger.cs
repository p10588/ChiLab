using System;
using Chi.Gameplay.Quest;
using Chi.Gameplay.Triggers;
using UnityEngine;

public interface IQuestTrigger
{
    GameObject gameObject { get; }
    IQuestProcessor questProcessor { get; set; }
    void RegistorTriggerAction(Action enterAction, Action exitAction);
    void UnregistorTriggerAction();
}

public class QuestTrigger : MonoBehaviour, IQuestTrigger
{
    GameObject IQuestTrigger.gameObject => this.gameObject;

    IQuestProcessor IQuestTrigger.questProcessor { get; set; }

    void IQuestTrigger.RegistorTriggerAction(Action enterAction, Action exitAction) {
        throw new NotImplementedException();
    }

    void IQuestTrigger.UnregistorTriggerAction() {
        throw new NotImplementedException();
    }
}

