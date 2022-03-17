using UnityEngine;
using RPG.Dialogue;
using TMPro;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _aiText;
        #endregion



        #region --Fields-- (In Class)
        private PlayerConversant _playerConversant;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();

            _aiText.text = _playerConversant.GetText();
        }
        #endregion
    }
}