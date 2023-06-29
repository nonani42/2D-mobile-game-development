using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = nameof(CurrencySlotConfigDataSource),menuName = "Configs/" + "Rewards/" + nameof(CurrencySlotConfigDataSource))]
    internal class CurrencySlotConfigDataSource : ScriptableObject
    {
        [field: SerializeField] public CurrencySlotConfig[] CurrencySlotConfigs { get; private set; }
    }
}
