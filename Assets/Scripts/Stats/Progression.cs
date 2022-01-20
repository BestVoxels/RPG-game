using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/ New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        #region --Fields-- (Inspector)
        [SerializeField] private ProgressionCharacterType[] _chractersProgression;
        #endregion



        #region --Classes-- (Custom PRIVATE)
        [System.Serializable]
        private class ProgressionCharacterType
        {
            public CharacterType characterType;

            public int[] health;
            public int[] damage;
        }
        #endregion
    }
}