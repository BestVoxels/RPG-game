using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities
{
    public abstract class EffectStrategy : ScriptableObject
    {
        #region --Methods-- (Custom PUBLIC)
        public abstract void StartEffect(GameObject user, IEnumerable<GameObject> targets, Action onFinished);
        #endregion
    }
}