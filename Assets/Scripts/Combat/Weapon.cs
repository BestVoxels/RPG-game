using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        #region --Fields-- (Inspector)
        [SerializeField] private GameObject _weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController _animatorOverride = null;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Spawn(Transform handTransform, Animator animator)
        {
            animator.runtimeAnimatorController = _animatorOverride;

            Instantiate(_weaponPrefab, handTransform);
        }
        #endregion
    }
}