using System.Collections.Generic;
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
        private DialogueNode _previousNode;
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

        public string GetQuestionText()
        {
            if (_previousNode == null)
            {
                return "";
            }

            return _previousNode.QuestionText;
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            DialogueNode[] playerNode = _currentDialogue.GetPlayerChildren(_previousNode).ToArray();

            foreach (DialogueNode eachNode in playerNode)
            {
                yield return eachNode;
            }
        }

        public void GetChoiceNode(DialogueNode clickedNode)
        {
            _previousNode = _currentNode;
            _currentNode = clickedNode;

            GetNextNode();
        }

        public void GetNextNode()
        {
            if (!HasNext()) return;

            //int numPlayerResponses = _currentDialogue.GetPlayerChildren(_currentNode).Count();
            //if (numPlayerResponses > 0)
            //{
            //    _isResponsePanel = true;
            //    return;
            //}

            DialogueNode[] allNode = _currentDialogue.GetAllChildren(_currentNode).ToArray();

            DialogueNode randChild = allNode[Random.Range(0, allNode.Length)];

            _previousNode = _currentNode;
            _currentNode = randChild;
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildren(_currentNode).ToArray().Length > 0;
        }

        public bool IsPlayerSpeaking()
        {
            return _currentNode.Speaker == DialogueSpeaker.Player;
        }
        #endregion
    }
}