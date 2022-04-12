using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Shops;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Row Stuffs")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _availabilityText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _quantityText;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Setup(ShopItem shopItem)
        {
            _iconImage.overrideSprite = shopItem.Icon;
            _nameText.text = shopItem.Name;
            _availabilityText.text = $"{shopItem.Availability}";
            _priceText.text = $"{shopItem.Price:N0}";
            _quantityText.text = $"{shopItem.QuantityInTransaction}";
        }
        #endregion
    }
}