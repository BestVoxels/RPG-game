using UnityEngine;
using TMPro;
using RPG.Quests;
using System.Linq;

namespace RPG.UI.Quests
{
    public class QuestTooltip : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Main Header Texts")]
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _descriptionText;

        [Header("Sub Header Texts")]
        [SerializeField] private TMP_Text _objectiveHeaderTextPrefab;
        [SerializeField] private TMP_Text _rewardHeaderTextPrefab;

        [Header("Spawner Stuff")]
        [SerializeField] private GameObject _objectiveCompletePrefab;
        [SerializeField] private GameObject _objectiveIncompletePrefab;
        [SerializeField] private Transform _objectiveTransform;
        [SerializeField] private GameObject _rewardPrefab;
        [SerializeField] private Transform _rewardTransform;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Setup(QuestStatus questStatus)
        {
            _headerText.text = questStatus.Quest.Title;
            _descriptionText.text = questStatus.Quest.Description;

            ClearObjectiveList();
            ClearRewardList();

            BuildObjectiveList(questStatus);
            BuildRewardList(questStatus);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void BuildObjectiveList(QuestStatus questStatus)
        {
            Instantiate(_objectiveHeaderTextPrefab, _objectiveTransform);

            foreach (Quest.Objective eachObjective in questStatus.Quest.Objectives)
            {
                GameObject objectivePrefab = questStatus.IsObjectiveCompleted(eachObjective.referenceID) ? _objectiveCompletePrefab : _objectiveIncompletePrefab;

                TMP_Text createdPrefabText = Instantiate(objectivePrefab, _objectiveTransform).GetComponentInChildren<TMP_Text>();
                createdPrefabText.text = eachObjective.description;
            }
        }

        private void BuildRewardList(QuestStatus questStatus)
        {
            if (questStatus.Quest.Rewards.Count() == 0)
                return;

            Instantiate(_rewardHeaderTextPrefab, _rewardTransform);

            foreach (Quest.Reward eachReward in questStatus.Quest.Rewards)
            {
                TMP_Text createdPrefabText = Instantiate(_rewardPrefab, _rewardTransform).GetComponentInChildren<TMP_Text>();
                createdPrefabText.text = eachReward.description;
            }
        }

        private void ClearObjectiveList()
        {
            foreach (Transform eachChild in _objectiveTransform)
                Destroy(eachChild.gameObject);
        }

        private void ClearRewardList()
        {
            foreach (Transform eachChild in _rewardTransform)
                Destroy(eachChild.gameObject);
        }
        #endregion
    }
}