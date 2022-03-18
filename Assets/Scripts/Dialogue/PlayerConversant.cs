using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Dialogue _currentDialogue;
        #endregion



        #region --Fields-- (In Class)
        private DialogueNode _currentNode;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _currentNode = _currentDialogue.GetRootNode();
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public string GetText()
        {
            if (_currentNode == null)
            {
                return "";
            }

            return _currentNode.Text;
        }

        public void Next()
        {
            if (HasNext())
            {
                DialogueNode[] childArray = _currentDialogue.GetAllChildren(_currentNode).ToArray();

                DialogueNode randChild = childArray[Random.Range(0, childArray.Length)];

                _currentNode = randChild;
            }
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildren(_currentNode).ToArray().Length > 0;
        }
        #endregion
    }
}