using UnityEngine;
using TMPro;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _timer;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Setup(Quest quest)
        {
            _title.text = quest.Title;
            _timer.text = $"{quest.TimerInHours / 24} Days"; // Use Method to Convert the time here.
        }
        #endregion
    }
}