using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Dialogue _testDialogue;
        #endregion



        #region --Events-- (Delegate as Action)
        public event Action OnDialogueUpdated;
        #endregion



        #region --Fields-- (In Class)
        private Dialogue _currentDialogue;
        private DialogueNode _currentNode;
        private DialogueNode _previousNode;
        #endregion



        #region --Methods-- (Built In)
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);

            StartDialogue(_testDialogue);
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void StartDialogue(Dialogue newDialogue)
        {
            _currentDialogue = newDialogue;
            _currentNode = _currentDialogue.GetRootNode();

            OnDialogueUpdated?.Invoke();
        }

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

            OnDialogueUpdated?.Invoke();
            GetNextNode();
        }

        public void GetNextNode()
        {
            if (!HasNext()) return;

            // Randomly get Node from All Children
            DialogueNode[] allNode = _currentDialogue.GetAllChildren(_currentNode).ToArray();
            DialogueNode randChild = allNode[UnityEngine.Random.Range(0, allNode.Length)];

            _previousNode = _currentNode;
            _currentNode = randChild;

            OnDialogueUpdated?.Invoke();
        }

        public bool HasNext()
        {
            return _currentDialogue.GetAllChildren(_currentNode).ToArray().Length > 0;
        }

        public bool IsActive()
        {
            return _currentDialogue != null;
        }

        public bool IsPlayerSpeaking()
        {
            return _currentNode.Speaker == DialogueSpeaker.Player;
        }
        #endregion
    }
}