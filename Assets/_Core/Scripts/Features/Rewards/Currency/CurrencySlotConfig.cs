using UnityEngine;

namespace Features.Rewards.Currency
{
    internal interface ICurrency
    {
        RewardType Type { get; }
        Sprite Icon { get; }
    }

    [CreateAssetMenu(fileName = nameof(CurrencySlotConfig), menuName = "Configs/" + "Rewards/" + nameof(CurrencySlotConfig))]
    internal class CurrencySlotConfig : ScriptableObject, ICurrency
    {
        [field: SerializeField] public RewardType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
