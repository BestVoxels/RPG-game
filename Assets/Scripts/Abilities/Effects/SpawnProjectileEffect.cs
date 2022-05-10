using System;
using UnityEngine;
using RPG.Combat;
using RPG.Attributes;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Untitled Spawn Projectile Effect", menuName = "RPG/Game Item/Effects/New Spawn Projectile Effect", order = 133)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        #region --Fields-- (Inspector)
        [SerializeField] private Projectile _projectile;
        [Tooltip("Only provide positive value, to deduct health with amount provide.")]
        [SerializeField] private float _damage = 0f;
        [Tooltip("Using Hands Transform that provided in Fighter script. (to add more spawning point, Check WeaponConfig for how to do that.)")]
        [SerializeField] private bool _isRightHanded = true;
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private Transform GetTransform(Transform rightHand, Transform leftHand) => _isRightHanded ? rightHand : leftHand;
        #endregion



        #region --Methods-- (Override)
        public override void StartEffect(AbilityData data, Action<string> onFinished)
        {
            Fighter fighter = data.User.transform.root.GetComponentInChildren<Fighter>();
            if (fighter == null) return;
            Transform handTransform = GetTransform(fighter.RightHandTransform, fighter.LeftHandTransform);

            foreach (GameObject target in data.Targets)
            {
                Health health = target.GetComponentInChildren<Health>();
                if (health == null || health.IsDead) continue;

                Projectile projectileCloned = Instantiate(_projectile, handTransform.position, handTransform.rotation);
                projectileCloned.SetTarget(data.User, health, _damage);
            }

            onFinished?.Invoke(name);
        }
        #endregion
    }
}