using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;

namespace RPG.Traits
{
    public class TraitStore : MonoBehaviour
    {
        #region --Events-- (Delegate as Action)
        public event Action OnPointsChanged;
        #endregion



        #region --Fields-- (In Class)
        private BaseStats _baseStats;

        private readonly Dictionary<Trait, int> _committedPoints = new Dictionary<Trait, int>();
        private readonly Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _baseStats = transform.root.GetComponentInChildren<BaseStats>();
        }

        private void OnEnable()
        {
            _baseStats.OnLevelUp += OnPointsChanged;
        }

        private void OnDisable()
        {
            _baseStats.OnLevelUp -= OnPointsChanged;
        }
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Staged/Committed/Combined Points~
        public int GetCommittedPoints(Trait trait) => _committedPoints.ContainsKey(trait) ? _committedPoints[trait] : 0;

        public int GetStagedPoints(Trait trait) => _stagedPoints.ContainsKey(trait) ? _stagedPoints[trait] : 0;

        public int GetCombinedPoints(Trait trait) => GetCommittedPoints(trait) + GetStagedPoints(trait);
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Unallocated/TotalCombined/TotalAvailable/ Points~
        public int GetUnallocatedPoints() => GetTotalAvailablePoints() - GetTotalCombinedPoints();

        public int GetTotalCombinedPoints()
        {
            int totalPoints = 0;

            foreach (Trait trait in Enum.GetValues(typeof(Trait))) // Get All Enum Values
            {
                totalPoints += GetCombinedPoints(trait);
            }

            return totalPoints;
        }

        public int GetTotalAvailablePoints()
        {
            return (int)_baseStats.GetTotalTraitPoint();
        }
        #endregion



        #region --Methods-- (Custom PUBLIC) ~Staging~
        public void StagePoints(Trait trait, int points)
        {
            if (!CanStagePoints(trait, points)) return;

            _stagedPoints[trait] = GetStagedPoints(trait) + points;
            //_unallocatedPoints -= points;

            if (GetStagedPoints(trait) == 0) _stagedPoints.Remove(trait);
            OnPointsChanged?.Invoke();
        }

        public bool CanStagePoints(Trait trait, int points)
        {
            if (points > GetUnallocatedPoints()) return false;
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
            OnPointsChanged?.Invoke();
        }

        public bool CanCommit() => _stagedPoints.Count > 0;
        #endregion
    }
}