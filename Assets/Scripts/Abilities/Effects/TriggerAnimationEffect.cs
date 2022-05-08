using System;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Untitled Trigger Animation Effect", menuName = "RPG/Game Item/Effects/New Trigger Animation Effect", order = 130)]
    public class TriggerAnimationEffect : EffectStrategy
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _triggerCondition = "UseAbility1";
        #endregion



        #region --Methods-- (Override)
        public override void StartEffect(AbilityData data, Action<string> onFinished)
        {
            Animator animator = data.User.transform.root.GetComponentInChildren<Animator>();

            animator.SetTrigger(_triggerCondition);

            onFinished?.Invoke(name);
        }
        #endregion
    }
}