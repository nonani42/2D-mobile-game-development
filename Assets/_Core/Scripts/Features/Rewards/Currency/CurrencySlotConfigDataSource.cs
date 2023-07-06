using UnityEngine;

namespace Features.Rewards.Currency
{
    [CreateAssetMenu(fileName = nameof(CurrencySlotConfigDataSource),menuName = "Configs/" + "Rewards/" + nameof(CurrencySlotConfigDataSource))]
    internal class CurrencySlotConfigDataSource : ScriptableObject
    {
        [field: SerializeField] public CurrencySlotConfig[] CurrencySlotConfigs { get; private set; }
    }
}
