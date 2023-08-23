using System;
using UnityEngine;

namespace Chi.Gameplay.Quest
{

    public interface IQuestTrigger
    {
        IQuestProcessor questProcessor { get; }
        void InitalizeTrigger(IQuestProcessor questProcessor);
        void UninitalizeTrigger();
    }
}