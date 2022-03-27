using System.Collections.Generic;

namespace RPG.Quests
{
    public class QuestStatus
    {
        #region --Fields-- (In Class)
        private Quest _quest;
        private List<string> _completedObjectives = new List<string>();
        #endregion



        #region --Properties-- (With Backing Fields)
        public Quest Quest { get { return _quest; } }
        public int CompletedCount { get { return _completedObjectives.Count; } }
        #endregion



        #region --Constructors-- (PUBLIC)
        public QuestStatus(Quest quest)
        {
            _quest = quest;
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool IsObjectiveCompleted(string compareObjective) => _completedObjectives.Contains(compareObjective);
        #endregion
    }
}