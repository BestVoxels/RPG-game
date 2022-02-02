using UnityEngine;
using TMPro;

namespace RPG.UI.InGame
{
    public class DamageText : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _text;
        #endregion



        #region --Properties-- (With Backing Fields)
        public string text { get { return _text.text; } set { _text.text = value; } }
        #endregion
    }
}