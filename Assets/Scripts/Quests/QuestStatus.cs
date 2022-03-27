using System.Collections.Generic;
using System.Linq;

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
        public bool AddCompletedObjective(string objectiveToAdd)
        {
            if (IsObjectiveCompleted(objectiveToAdd)) return false;
            if (!Quest.IsObjectiveExist(objectiveToAdd))
            {
                UnityEngine.Debug.LogError($"There is no '{objectiveToAdd}' Objective in the '{Quest.name}' Quest's Objectives List");
                return false;
            }

            _completedObjectives.Add(objectiveToAdd);

            if (IsQuestCompleted())
            {
                UnityEngine.Debug.Log($"{Quest.Title} is Completed! GIVE OUT REWARD!");
            }

            return true;
        }

        public bool IsObjectiveCompleted(string compareObjective) => _completedObjectives.Contains(compareObjective);

        public bool IsQuestCompleted() => Quest.Objectives.Count() == CompletedCount;
        #endregion
    }
}