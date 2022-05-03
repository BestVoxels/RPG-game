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
        [SerializeField] private EffectStrategy[] _effectStrategies;
        #endregion



        #region --Methods-- (Override)
        public override void Use(GameObject user)
        {
            if (_targetingStrategy == null) return;

            _targetingStrategy.StartTargeting(user, (IEnumerable<GameObject> targets) => OnTargetAquired(user, targets));
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void OnTargetAquired(GameObject user, IEnumerable<GameObject> targets)
        {
            foreach (FilterStrategy eachFilter in _filterStrategies)
            {
                targets = eachFilter.Filter(targets);
            }

            foreach (EffectStrategy eachEffect in _effectStrategies)
            {
                eachEffect.StartEffect(user, targets, OnEffectFinished);
            }
        }

        private void OnEffectFinished()
        {
            Debug.Log("On Effect Finished");
        }
        #endregion
    }
}