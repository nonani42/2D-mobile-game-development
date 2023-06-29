using System.Collections.Generic;
using UnityEngine;

namespace Rewards
{
    [CreateAssetMenu(fileName = nameof(RewardSlotConfigDataSource), menuName = "Configs/" + "Rewards/" + nameof(RewardSlotConfigDataSource))]
    class RewardSlotConfigDataSource : ScriptableObject
    {
        [field: SerializeField] public RewardPeriodType Period { get; private set; }

        [field: SerializeField] public float TimeCooldown { get; private set; }
        [field: SerializeField] public float TimeDeadline { get; private set; }

        [field: SerializeField] public RewardSlotConfig[] RewardSlotConfigs { get; private set; }
    }
}
