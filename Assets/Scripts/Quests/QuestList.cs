using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] QuestStatus[] _questStatuses;
        #endregion



        #region --Properties-- (With Backing Fields)
        public IEnumerable<QuestStatus> QuestStatuses { get { return _questStatuses; } }
        #endregion
    }
}