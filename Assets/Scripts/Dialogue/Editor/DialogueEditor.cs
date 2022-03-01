using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        #region --Methods-- (Built In)
        private void OnGUI()
        {
            //EditorGUI.LabelField(new Rect(10, 10, 100, 10), "YOOOOO WASSAP!");
            EditorGUILayout.LabelField("Apple");
            EditorGUILayout.LabelField("Pear");
            EditorGUILayout.LabelField("Mango");
        }
        #endregion



        #region --Methods-- (Custom PRIVATE) ~Call Through Callback~
        [MenuItem("Window/RPG/Dialogue Window", false, 10000)]
        private static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue");
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
    }
}