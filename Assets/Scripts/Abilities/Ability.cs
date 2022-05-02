using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "Untitled Ability", menuName = "RPG/Game Item/New Ability (Action)", order = 124)]
    public class Ability : ActionItem
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TargetingStrategy _targetingStrategy;
        [SerializeField] private FilterStrategy[] _filterStrategies;
        #endregion



        #region --Methods-- (Override)
        public override void Use(GameObject user)
        {
            if (_targetingStrategy == null) return;

            _targetingStrategy.StartTargeting(user, OnTargetAquired);
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void OnTargetAquired(IEnumerable<GameObject> targets)
        {
            Debug.Log("Target acquired as follows...");

            foreach (FilterStrategy eachFilter in _filterStrategies)
            {
                targets = eachFilter.Filter(targets);
            }

            foreach (GameObject target in targets)
            {
                Debug.Log(target.name);
            }
        }
        #endregion
    }
}