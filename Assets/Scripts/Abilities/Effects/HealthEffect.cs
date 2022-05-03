using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Untitled Health Effect", menuName = "RPG/Game Item/Effects/New Health Effect", order = 129)]
    public class HealthEffect : EffectStrategy
    {
        #region --Fields-- (Inspector)
        [Tooltip("Positive Number will Heal the target / Negative Number will Deal Damage to the target.")]
        [SerializeField] private float _healthPointsAddOn = 0f;
        #endregion



        #region --Methods-- (Override)
        public override void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action onFinished)
        {
            foreach (GameObject target in targets)
            {
                Health targetHealth = target.GetComponentInChildren<Health>();
                if (targetHealth == null) continue;

                if (_healthPointsAddOn < 0)
                    targetHealth.TakeDamage(user, -_healthPointsAddOn); // do minus again to make it positive since method expect positive value.
                else
                    targetHealth.Heal(_healthPointsAddOn);
            }

            onFinished?.Invoke();
        }
        #endregion
    }
}