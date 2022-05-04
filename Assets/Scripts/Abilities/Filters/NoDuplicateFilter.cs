using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    [CreateAssetMenu(fileName = "Untitled NoDuplicate Filter", menuName = "RPG/Game Item/Filters/New NoDuplicate Filter", order = 128)]
    public class NoDuplicateFilter : FilterStrategy
    {
        #region --Fields-- (In Class)
        private List<GameObject> _gameObjectsDatabase = new List<GameObject>();
        #endregion



        #region --Methods-- (Override)
        public override IEnumerable<GameObject> Filter(AbilityData dataToFilter)
        {
            _gameObjectsDatabase.Clear(); // have to clear out each time otherwise second time no object will be returned

            foreach (GameObject eachObject in dataToFilter.Targets)
            {
                if (!_gameObjectsDatabase.Contains(eachObject))
                {
                    _gameObjectsDatabase.Add(eachObject);
                    yield return eachObject;
                }
            }
        }
        #endregion
    }
}