using Profile;
using System;
using UnityEngine;

namespace CarGame
{
    [CreateAssetMenu(fileName = nameof(InitialSettingsConfig), menuName = "Configs/" + nameof(InitialSettingsConfig))]
    internal sealed class InitialSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public GameState InitialState { get; private set; }
        [field: SerializeField] public InitialCar Car { get; private set; }
    }

    [Serializable]
    internal sealed class InitialCar
    {
        [field: SerializeField] public float SpeedCar { get; private set; }
        [field: SerializeField] public float JumpHeightCar { get; private set; }
    }
}
