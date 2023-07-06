using Features.Rewards.Currency;
using UnityEngine;

namespace Features.Rewards
{
    [CreateAssetMenu(fileName = nameof(RewardSlotConfig), menuName = "Configs/" + "Rewards/" + nameof(RewardSlotConfig))]
    internal class RewardSlotConfig : ScriptableObject
    {
        [field: SerializeField] public CurrencySlotConfig CurrencySlotConfig { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
