using UnityEngine;
using UnityEngine.UI;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _aiText;
        [SerializeField] private Button _nextButton;
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
            UpdateAiText();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void UpdateAiText()
        {
            _aiText.text = _playerConversant.GetText();
            _nextButton.gameObject.SetActive(_playerConversant.HasNext());
        }
        #endregion



        #region --Methods-- (Subscriber) ~UnityEvent~
        private void Next()
        {
            _playerConversant.Next();
            UpdateAiText();
        }
        #endregion
    }
}