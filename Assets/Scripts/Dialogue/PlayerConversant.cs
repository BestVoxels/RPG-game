using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Dialogue _currentDialogue;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public string GetText()
        {
            DialogueNode rootNode = _currentDialogue.GetRootNode();

            return rootNode.Text;
        }
        #endregion
    }
}