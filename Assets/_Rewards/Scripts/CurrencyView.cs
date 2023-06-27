using UnityEngine;

namespace Rewards
{
    internal class CurrencyView : MonoBehaviour
    {
        private const string WoodKey = nameof(WoodKey);
        private const string CrystalKey = nameof(CrystalKey);

        private static CurrencyView _instance;
        public static CurrencyView Instance => _instance;

        [SerializeField] private CurrencySlotView _currencyWood;
        [SerializeField] private CurrencySlotView _currencyCrystal;

        private int Wood
        {
            get => PlayerPrefs.GetInt(WoodKey);
            set => PlayerPrefs.SetInt(WoodKey, value);
        }

        private int Crystal
        {
            get => PlayerPrefs.GetInt(CrystalKey);
            set => PlayerPrefs.SetInt(CrystalKey, value);
        }


        private void Awake() =>
            _instance = this;

        private void OnDestroy() =>
            _instance = null;

        private void Start()
        {
            _currencyWood.SetData(Wood);
            _currencyCrystal.SetData(Crystal);
        }


        public void AddWood(int value)
        {
            Wood += value;
            _currencyWood.SetData(Wood);
        }

        public void AddCrystal(int value)
        {
            Crystal += value;
            _currencyCrystal.SetData(Crystal);
        }
    }
}
