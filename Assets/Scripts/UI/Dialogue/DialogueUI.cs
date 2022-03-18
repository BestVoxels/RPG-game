using UnityEngine;
using UnityEngine.UI;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Dialogue Panels")]
        [SerializeField] private GameObject[] _dialoguePanels;

        [Header("Response Panels")]
        [SerializeField] private GameObject[] _responsePanels;

        [Space]
        [Space]

        [Header("Dialogue Stuffs")]
        [SerializeField] private TMP_Text _dialogueText;
        [SerializeField] private Button _nextButton;

        [Header("Response Stuffs")]
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private GameObject _replyButtonPrefab;
        [SerializeField] private Transform _spawnParent;
        #endregion



        #region --Fields-- (In Class)
        private PlayerConversant _playerConversant;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();

            _nextButton.onClick.AddListener(Next);
        }

        private void Start()
        {
            UpdateDialogueUI();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void UpdateDialogueUI()
        {
            if (_playerConversant.IsPlayerSpeaking())
            {
                SetDialoguePanels(false);
                SetResponsePanels(true);

                _questionText.text = _playerConversant.GetQuestionText();

                ClearReplyButton();
                BuildChoiceList();
            }
            else
            {
                SetResponsePanels(false);
                SetDialoguePanels(true);

                _dialogueText.text = _playerConversant.GetText();
            }

            _nextButton.gameObject.SetActive(_playerConversant.HasNext());
        }

        private void ClearReplyButton()
        {
            foreach (Transform eachButton in _spawnParent.transform)
            {
                Destroy(eachButton.gameObject);
            }
        }

        private void BuildChoiceList()
        {
            foreach (DialogueNode choiceNode in _playerConversant.GetChoices())
            {
                GameObject spawnedGameObject = Instantiate(_replyButtonPrefab, _spawnParent);
                spawnedGameObject.GetComponentInChildren<TMP_Text>().text = choiceNode.Text;
                spawnedGameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { Pick(choiceNode); });
            }
        }

        private void SetDialoguePanels(bool status)
        {
            foreach (GameObject eachPanel in _dialoguePanels)
                eachPanel.SetActive(status);
        }

        private void SetResponsePanels(bool status)
        {
            foreach (GameObject eachPanel in _responsePanels)
                eachPanel.SetActive(status);
        }
        #endregion



        #region --Methods-- (Subscriber) ~UnityEvent~
        private void Next()
        {
            _playerConversant.GetNextNode();
            UpdateDialogueUI();
        }

        private void Pick(DialogueNode selectedNode)
        {
            _playerConversant.GetChoiceNode(selectedNode);
            UpdateDialogueUI();
        }
        #endregion
    }
}