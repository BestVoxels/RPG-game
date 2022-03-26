using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Unnamed Quest", menuName = "RPG/Quest/New Quest")]
    public class Quest : ScriptableObject
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _title;
        [SerializeField] private int _timerInHours;
        [SerializeField] private string[] _objectives;
        #endregion



        #region --Properties-- (With Backing Up)
        public string Title { get { return _title; } }
        public int TimerInHours { get { return _timerInHours; } }
        #endregion
    }
}