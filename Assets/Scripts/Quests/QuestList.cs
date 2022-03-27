using System.Collections.Generic;
using System;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour
    {
        #region --Events-- (Delegate as Action)
        public event Action OnQuestListUpdated;
        #endregion



        #region --Fields-- (In Class)
        private List<QuestStatus> _questStatuses = new List<QuestStatus>();
        #endregion



        #region --Properties-- (With Backing Fields)
        public IEnumerable<QuestStatus> QuestStatuses { get { return _questStatuses; } }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void AddQuest(Quest questToAdd)
        {
            if (IsQuestExist(questToAdd)) return;

            QuestStatus newQuestStatus = new QuestStatus(questToAdd);
            _questStatuses.Add(newQuestStatus);

            OnQuestListUpdated?.Invoke();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private bool IsQuestExist(Quest questToCheck)
        {
            foreach (QuestStatus eachQuestStatus in QuestStatuses)
            {
                if (eachQuestStatus.Quest == questToCheck)
                    return true;
            }

            return false;
        }
        #endregion
    }
}