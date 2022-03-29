using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [System.Serializable]
    public class Condition
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _predicate;
        [SerializeField] private string[] _parameters;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
        {
            foreach (IPredicateEvaluator each in evaluators)
            {
                bool? result = each.Evaluate(_predicate, _parameters);

                if (result == null) continue;

                if (result == false) return false;
            }

            return true;
        }
        #endregion
    }
}