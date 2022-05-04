using System;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    [CreateAssetMenu(fileName = "Untitled Demo Targeting", menuName = "RPG/Game Item/Targeting/New Demo", order = 125)]
    public class DemoTargeting : TargetingStrategy
    {
        #region --Methods-- (Override)
        public override void StartTargeting(AbilityData data, Action onFinished)
        {
            Debug.Log("Demo Targeting Started");
        }
        #endregion
    }
}