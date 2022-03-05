using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        #region --Fields-- (In Class)
        private Dialogue _selectedDialogue = null;
        private GUIStyle _nodeStyle = null;
        private DialogueNode _draggingNode = null;
        private Vector2 _clickOffSet = new Vector2();
        #endregion



        #region --Methods-- (Annotation)
        [MenuItem("Window/RPG/Dialogue Window", false, 10000)]
        private static void ShowEditorWindow()
        {
            DialogueEditor dialogueEditor = GetWindow(typeof(DialogueEditor), false, "Dialogue") as DialogueEditor;
            dialogueEditor.RefreshDialogueWindow();
        }

        [OnOpenAsset(0)]
        private static bool OnOpenAsset(int instanceID, int line)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is Dialogue)
            {
                ShowEditorWindow();
                return true;
            }

            return false;
        }
        #endregion



        #region --Methods-- (Built In)
        private void OnEnable()
        {
            _nodeStyle = new GUIStyle();
            _nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            _nodeStyle.padding = new RectOffset(15, 15, 15, 15);
            _nodeStyle.border = new RectOffset(12, 12, 12, 12);
            //_nodeStyle.normal.textColor = Color.white; // Not get any effect
        }

        private void OnGUI()
        {
            if (_selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                ProcessEvents();

                // Drawing Bezier Curve first BEFORE Drawing Nodes to make Nodes stay infront of Curves
                foreach (DialogueNode eachNode in _selectedDialogue.Nodes)
                {
                    DrawConnections(eachNode);
                }
                foreach (DialogueNode eachNode in _selectedDialogue.Nodes)
                {
                    DrawNode(eachNode);
                }
            }
        }

        private void OnSelectionChange()
        {
            RefreshDialogueWindow();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && _draggingNode == null)
            {
                _draggingNode = GetNodeAtPoint(Event.current.mousePosition);

                if (_draggingNode != null)
                    _clickOffSet = _draggingNode.rect.position - Event.current.mousePosition;
            }
            else if (Event.current.type == EventType.MouseDrag && _draggingNode != null)
            {
                Undo.RecordObject(_selectedDialogue, "Update Dialogue Position");

                _draggingNode.rect.position = Event.current.mousePosition + _clickOffSet;

                Repaint();
            }
            else if (Event.current.type == EventType.MouseUp && _draggingNode != null)
            {
                _draggingNode = null;

                EditorUtility.SetDirty(_selectedDialogue);
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            foreach (DialogueNode eachNode in _selectedDialogue.Nodes.Reverse()) // Reverse loop so that it pick the upper layer node first which is that lower bottom of the list
            {
                if (eachNode.rect.Contains(point))
                {
                    return eachNode;
                }
            }

            
            GUI.FocusControl(null);
            Repaint();
            
            return null;
        }

        private void DrawNode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, _nodeStyle);

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Node : ", EditorStyles.whiteLargeLabel);
            string newID = EditorGUILayout.TextField($"{node.uniqueID}");
            string newText = EditorGUILayout.TextField($"{node.text}");

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_selectedDialogue, "Update Dialogue Field"); // This will undo only one at a time because once changes is made on any of the field this will trigger right away

                node.text = newText;
                node.uniqueID = newID;

                EditorUtility.SetDirty(_selectedDialogue);
            }

            GUILayout.EndArea();
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector2(node.rect.xMax, node.rect.center.y);

            foreach (DialogueNode eachChildNode in _selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector2(eachChildNode.rect.xMin, eachChildNode.rect.center.y);

                // Making Curves
                Vector3 curveOffset = endPosition - startPosition; // Make it vaires according to the distance between two nodes
                curveOffset.y = 0; // no need to do curve on Y axis (only X axis)
                curveOffset.x *= 0.8f; // reduce 'curve looks' by 20%

                Handles.DrawBezier(
                    startPosition,
                    endPosition,
                    startPosition + curveOffset,
                    endPosition - curveOffset,
                    Color.white, null, 4f);
            }
        }

        private void RefreshDialogueWindow()
        {
            if (Selection.activeObject is Dialogue newDialogue)
            {
                _selectedDialogue = newDialogue;
                Repaint();
            }
            else
            {
                _selectedDialogue = null;
                Repaint();
            }
        }
        #endregion
    }
}