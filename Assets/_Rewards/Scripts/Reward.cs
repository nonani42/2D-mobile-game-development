using System;
using UnityEngine;

namespace Rewards
{
    [Serializable]
    internal class Reward
    {
        [field: SerializeField] public RewardType RewardType { get; private set; }
        [field: SerializeField] public Sprite CurrencyIcon { get; private set; }
        [field: SerializeField] public int CurrencyCount { get; private set; }
    }
}