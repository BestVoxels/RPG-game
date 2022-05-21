using System;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;
using RPG.Saving;

namespace RPG.Traits
{
    public class TraitStore : MonoBehaviour, IModifierProvider, ISaveable
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TraitBonus[] _traitBonusConfig;
        #endregion



        #region --Events-- (Delegate as Action)
        public event Action OnPointsChanged;
        #endregion



        #region --Fields-- (In Class)
        private BaseStats _baseStats;

        private Dictionary<Trait, int> _committedPoints = new Dictionary<Trait, int>();
        private Dictionary<Trait, int> _stagedPoints = new Dictionary<Trait, int>();

        private Dictionary<StatType, Dictionary<Trait, float>> _additiveBonusTable = null;
        private Dictionary<StatType, Dictionary<Trait, float>> _percentageBonusTable = null;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _baseStats = transform.root.GetComponentInChildren<BaseStats>();
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



        #region --Methods-- (Custom PRIVATE) ~Lookup Table~
        private void CreateLookupTable()
        {
            if (_additiveBonusTable != null || _percentageBonusTable != null) return; // Only Create Table Once
            
            _additiveBonusTable = new Dictionary<StatType, Dictionary<Trait, float>>();
            _percentageBonusTable = new Dictionary<StatType, Dictionary<Trait, float>>();

            foreach (TraitBonus eachConfig in _traitBonusConfig)
            {
                // Making IF check here so a single StatType can contains multiple Trait
                if (!_additiveBonusTable.ContainsKey(eachConfig.statType))
                    _additiveBonusTable.Add(eachConfig.statType, new Dictionary<Trait, float>());

                if (!_percentageBonusTable.ContainsKey(eachConfig.statType))
                    _percentageBonusTable.Add(eachConfig.statType, new Dictionary<Trait, float>());

                _additiveBonusTable[eachConfig.statType].Add(eachConfig.trait, eachConfig.additiveBonusPerPoint);
                _percentageBonusTable[eachConfig.statType].Add(eachConfig.trait, eachConfig.percentageBonusPerPoint);
            }
        }
        #endregion



        #region --Methods-- (Interface)
        IEnumerable<float> IModifierProvider.GetAdditiveModifiers(StatType statType)
        {
            CreateLookupTable();
            if (!_additiveBonusTable.ContainsKey(statType)) yield break;

            foreach (Trait traitOfStat in _additiveBonusTable[statType].Keys)
            {
                float bonus = _additiveBonusTable[statType][traitOfStat];
                yield return GetCommittedPoints(traitOfStat) * bonus;
            }
        }

        IEnumerable<float> IModifierProvider.GetPercentageModifiers(StatType statType)
        {
            CreateLookupTable();
            if (!_percentageBonusTable.ContainsKey(statType)) yield break;

            foreach (Trait traitOfStat in _percentageBonusTable[statType].Keys)
            {
                float bonus = _percentageBonusTable[statType][traitOfStat];
                yield return GetCommittedPoints(traitOfStat) * bonus;
            }
        }

        object ISaveable.CaptureState()
        {
            return _committedPoints;
        }

        void ISaveable.RestoreState(object state)
        {
            _committedPoints = new Dictionary<Trait, int>((Dictionary<Trait, int>)state);
            OnPointsChanged?.Invoke();
        }
        #endregion



        #region --Classes-- (Custom PRIVATE)
        [System.Serializable]
        private class TraitBonus
        {
            [Tooltip("One Trait can modify multiple Stat. (add more config element)")]
            public Trait trait;
            [Tooltip("Stat that will get effect by Trait above.")]
            public StatType statType;
            public float additiveBonusPerPoint = 0f;
            public float percentageBonusPerPoint = 0f;
        }
        #endregion
    }
}