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
        private readonly Dictionary<Trait, int> _committedPoints = new Dictionary<Trait, int>();
        private readonly Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();
        private int _unallocatedPoints = 10;
        #endregion



        #region --Properties-- (With Backing Fields)
        public int UnallocatedPoints { get => _unallocatedPoints; }
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Get Points~
        public int GetCommittedPoints(Trait trait) => _committedPoints.ContainsKey(trait) ? _committedPoints[trait] : 0;

        public int GetStagedPoints(Trait trait) => _stagedPoints.ContainsKey(trait) ? _stagedPoints[trait] : 0;

        public int GetCombinedPoints(Trait trait) => GetCommittedPoints(trait) + GetStagedPoints(trait);
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Staging~
        public void StagePoints(Trait trait, int points)
        {
            if (!CanStagePoints(trait, points)) return;

            _stagedPoints[trait] = GetStagedPoints(trait) + points;
            _unallocatedPoints -= points;

            if (GetStagedPoints(trait) == 0) _stagedPoints.Remove(trait);
            OnValueChanged?.Invoke();
        }

        public bool CanStagePoints(Trait trait, int points)
        {
            if (points > _unallocatedPoints) return false;
            if (GetStagedPoints(trait) + points < 0) return false;
            return true;
        }
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Committing~
        public void CommitPoints()
        {
            if (!CanCommit()) return;

            foreach (KeyValuePair<Trait, int> pair in _stagedPoints)
                _committedPoints[pair.Key] = GetCommittedPoints(pair.Key) + pair.Value;
            
            _stagedPoints.Clear();
            OnValueChanged?.Invoke();
        }

        public bool CanCommit() => _stagedPoints.Count > 0;
        #endregion
    }
}