using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chi.Gameplay.Quest {

    public enum QuestType
    {
        Normal,
    }

    public enum QuestProcState
    {
        NonActive = 0,
        Active,
        InProgress,
        Resolve
    }

    public interface IQuestProcessor {
        QuestType QuestProcessorType { get; }
        QuestData QuestData { get; }
        QuestProcState CurQuestState { get; }
        QuestManagerProcessor QuestManagerProcessor { get; }
        void ChangeProc(QuestProcState state);
    }

    public class QuestProcessor : IQuestProcessor
    {
        QuestType IQuestProcessor.QuestProcessorType => QuestType.Normal;
        QuestProcState IQuestProcessor.CurQuestState => this._curQuestState;
        QuestData IQuestProcessor.QuestData => this._questData;
        QuestManagerProcessor IQuestProcessor.QuestManagerProcessor => this._questManagerProcessor;

        private QuestData _questData;
        private QuestProcState _curQuestState;
        private IQuestProc _curQuestProc;
        private Dictionary<QuestProcState, IQuestProc> _dicQuestProc;
        private QuestManagerProcessor _questManagerProcessor;

        public QuestProcessor(QuestData questData , QuestManagerProcessor questMgrProcessor) {
            this._questData = questData;
            this._questManagerProcessor = questMgrProcessor;
            this.InitalDicQuestProc();
        }

        private void InitalDicQuestProc() {
            if (this._dicQuestProc == null) {
                this._dicQuestProc = new Dictionary<QuestProcState, IQuestProc>();
                this._dicQuestProc.Add(QuestProcState.NonActive, null);
                this._dicQuestProc.Add(QuestProcState.Active, new ActiveProc(this));
                this._dicQuestProc.Add(QuestProcState.InProgress, new InProgressProc(this));
                this._dicQuestProc.Add(QuestProcState.Resolve, new ResolveProc(this));
            }
        }


        #region IQuestObjectProcessor Implementation

        void IQuestProcessor.ChangeProc(QuestProcState state) {
            this.SwitchQuestProc(state);
        }

        private void SwitchQuestProc(QuestProcState state) {
            if (this._dicQuestProc.TryGetValue(state, out IQuestProc questProc)) {
                this._curQuestProc?.Leave();

                this._curQuestState = state;
                this._curQuestProc = questProc;
                this._curQuestProc?.Entry();
            }
        }

        #endregion

    }
}