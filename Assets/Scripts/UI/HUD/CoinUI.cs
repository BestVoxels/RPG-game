using System.Globalization;
using UnityEngine;
using TMPro;
using RPG.Economy;

namespace RPG.UI.HUD
{
    public class CoinUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private TMP_Text _coinPointsText;
        #endregion



        #region --Fields-- (In Class)
        private Coin _playerCoin;
        #endregion



        #region --Methods-- (Built In)
        private void Awake()
        {
            _playerCoin = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Coin>();
        }

        private void OnEnable()
        {
            _playerCoin.OnCoinPointsUpdated += RefreshUI;
        }

        private void Start()
        {
            RefreshUI();
        }

        private void OnDisable()
        {
            _playerCoin.OnCoinPointsUpdated -= RefreshUI;
        }
        #endregion



        #region --Methods-- (Subscriber)
        private void RefreshUI()
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            _coinPointsText.text = _playerCoin.CoinPoints.ToString("#,0", nfi);
        }
        #endregion
    }
}