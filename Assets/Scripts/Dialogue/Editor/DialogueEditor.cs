using UnityEditor;
using UnityEditor.Callbacks;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        #region --Methods-- (Custom PRIVATE) ~Call Through Editor~
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