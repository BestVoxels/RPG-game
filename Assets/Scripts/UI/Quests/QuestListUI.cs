using RPG.Quests;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private QuestItemUI _questPrefab;
        #endregion



        #region --Fields-- (In Class)
        private QuestList _questList;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        private void OnEnable()
        {
            _questList.OnQuestListUpdated += UpdateQuestListUI;
        }

        private void Start()
        {
            UpdateQuestListUI();
        }

        // Can't Have OnDiable() to unsubscribe Since this one will be closed by default and with button
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void BuildQuestList()
        {
            foreach (QuestStatus eachQuestStatus in _questList.QuestStatuses)
            {
                QuestItemUI createdPrefab = Instantiate(_questPrefab, transform);
                createdPrefab.Setup(eachQuestStatus);
            }
        }

        private void ClearQuestList()
        {
            foreach (Transform eachChild in transform)
                Destroy(eachChild.gameObject);
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void UpdateQuestListUI()
        {
            ClearQuestList();

            BuildQuestList();
        }
        #endregion
    }
}