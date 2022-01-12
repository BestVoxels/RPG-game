using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        #region --Fields-- (In Class)
        private const string _defaultSaveFile = "save";

        public static SavingWrapper Instance { get; private set; }
        #endregion



        #region --Methods-- (Built In)
        private void Awake() => Instance = this;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Save()
        {
            GetComponent<SavingSystem>().Save(_defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(_defaultSaveFile);
        }
        #endregion
    }
}