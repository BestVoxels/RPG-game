using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [System.Serializable]
    public class QuestStatus
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Quest _quest;
        [SerializeField] private List<string> _completedObjectives;
        #endregion



        #region --Properties-- (With Backing Fields)
        public Quest Quest { get { return _quest; } }
        public int CompletedCount { get { return _completedObjectives.Count; } }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool IsObjectiveCompleted(string compareObjective) => _completedObjectives.Contains(compareObjective);
        #endregion
    }
}