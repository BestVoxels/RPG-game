using UnityEngine;

namespace RPG.UI.MainMenu
{
    public class SaveLoadUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Spawn Stuffs")]
        [SerializeField] private GameObject _rowPrefab;
        [SerializeField] private Transform _spawnParent;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            ClearItemList();
            BuildItemList();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void BuildItemList()
        {
            //foreach (GameObject eachItem in )
            for (int i = 0; i < 10; i++)
            {
                GameObject createdPrefab = Instantiate(_rowPrefab, _spawnParent);
            }
        }

        private void ClearItemList()
        {
            foreach (Transform eachChild in _spawnParent)
                Destroy(eachChild.gameObject);
        }
        #endregion
    }
}