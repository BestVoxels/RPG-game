using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Traits
{
    public class TraitStore : MonoBehaviour
    {
        #region --Events-- (Delegate as Action)
        public event Action OnValueChanged;
        #endregion



        #region --Fields-- (In Class)
        private readonly Dictionary<Trait, int> _assignedPoints = new Dictionary<Trait, int>();
        private int _unassignedPoints = 10;
        #endregion



        #region --Properties-- (With Backing Fields)
        public int UnAssignedPoints { get => _unassignedPoints; }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public int GetPoint(Trait trait)
        {
            return _assignedPoints.ContainsKey(trait) ? _assignedPoints[trait] : 0;
        }

        public void AssignPoint(Trait trait, int points)
        {
            if (!CanAssignPoint(trait, points)) return;

            _assignedPoints[trait] = GetPoint(trait) + points;
            _unassignedPoints -= points;
            OnValueChanged?.Invoke();
        }

        public bool CanAssignPoint(Trait trait, int points)
        {
            if (points > _unassignedPoints) return false;
            if (GetPoint(trait) + points < 0) return false;
            return true;
        }
        #endregion
    }
}