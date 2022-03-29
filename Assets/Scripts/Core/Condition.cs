using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [System.Serializable]
    public class Condition
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _methodName;
        [SerializeField] private string[] _parameters;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator eachEvaluator in evaluators)
            {
                bool? result = eachEvaluator.Evaluate(_methodName, _parameters);

                if (result == null) continue;

                if (result == false) return false;
            }

            return true;
        }
        #endregion
    }
}