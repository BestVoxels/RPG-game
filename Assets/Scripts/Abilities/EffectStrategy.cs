using System;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        #region --Methods-- (Custom PUBLIC)
        public abstract void StartEffect(AbilityData user, Action onFinished);
        #endregion
    }
}