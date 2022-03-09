using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "Unnamed Dialogue", menuName = "RPG/Dialogue/New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        #region --Fields-- (Inspector)
        [SerializeField] private List<DialogueNode> _nodes = new List<DialogueNode>();
        #endregion



        #region --Fields-- (In Class)
        private Dictionary<string, DialogueNode> _nodeLookUpTable = new Dictionary<string, DialogueNode>();
        #endregion



        #region --Properties-- (With Backing Fields)
        public IEnumerable<DialogueNode> Nodes { get { return _nodes; } }
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
#if UNITY_EDITOR
            if (_nodes.Count == 0)
            {
                CreateChildNodeUnder(null);
            }
#endif
            UpdateLookUpTable();
        }

        private void OnValidate()
        {
            UpdateLookUpTable();
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public DialogueNode GetRootNode()
        {
            return _nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.Children)
            {
                if (!_nodeLookUpTable.ContainsKey(childID)) continue;

                yield return _nodeLookUpTable[childID];
            }
        }

        public void CreateChildNodeUnder(DialogueNode parentNode)
        {
            DialogueNode childNode = CreateInstance<DialogueNode>();
            childNode.name = System.Guid.NewGuid().ToString();
            childNode.Text = "Type dialogue script here...";

            if (parentNode != null)
                parentNode.Children.Add(childNode.name);

            _nodes.Add(childNode);
            
            UpdateLookUpTable();

            Undo.RegisterCreatedObjectUndo(childNode, "Create Dialogue Node");
        }

        public void DeleteThisNode(DialogueNode nodeToDelete)
        {
            _nodes.Remove(nodeToDelete);

            UpdateLookUpTable();

            CleanDanglingNode(nodeToDelete);

            Undo.DestroyObjectImmediate(nodeToDelete); // put this one on last line so that other can't use deleted one
        }

        public void LinkBothNodes(DialogueNode parentNode, DialogueNode childNode)
        {
            parentNode.Children.Add(childNode.name);
        }

        public bool IsBothNodesLinked(DialogueNode parentNode, DialogueNode childNode)
        {
            return parentNode.Children.Contains(childNode.name);
        }

        public void UnlinkBothNodes(DialogueNode parentNode, DialogueNode childNode)
        {
            parentNode.Children.Remove(childNode.name);
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void UpdateLookUpTable()
        {
            _nodeLookUpTable.Clear();

            foreach (DialogueNode eachParentNode in _nodes)
            {
                _nodeLookUpTable.Add(eachParentNode.name, eachParentNode);
            }
        }

        private void CleanDanglingNode(DialogueNode nodeToDelete)
        {
            // Remove this node from any of the other node's children list
            foreach (DialogueNode eachNode in _nodes)
            {
                eachNode.Children.Remove(nodeToDelete.name);
            }
        }
        #endregion
    }
}