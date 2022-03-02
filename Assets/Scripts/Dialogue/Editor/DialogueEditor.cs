using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        #region --Fields-- (In Class)
        private Dialogue _selectedDialogue = null;
        #endregion



        #region --Methods-- (Built In)
        private void OnGUI()
        {
            if (_selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                foreach (DialogueNode eachNode in _selectedDialogue.Nodes)
                {
                    string oldNodeText = eachNode.text;
                    eachNode.text = EditorGUILayout.TextField($"{oldNodeText}");

                    if (!oldNodeText.Equals(eachNode.text))
                    {
                        EditorUtility.SetDirty(_selectedDialogue);
                    }
                }
            }
        }

        private void OnSelectionChange()
        {
            RefreshDialogueWindow();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE) ~Annotation Callback~
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



        #region --Methods-- (Custom PRIVATE)
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