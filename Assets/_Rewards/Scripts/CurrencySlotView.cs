using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    internal class CurrencySlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _icon;

        public void SetData(int count) =>
            _count.text = count.ToString();

        public void SetIcon(Sprite sprite) =>
            _icon.sprite = sprite;
    }
}
