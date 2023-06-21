using UnityEngine;

namespace CarGame
{
    [CreateAssetMenu(fileName = nameof(InitialSettingsConfig), menuName = "Configs/" + nameof(InitialSettingsConfig))]
    internal sealed class InitialSettingsConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedCar { get; private set; }
        [field: SerializeField] public float JumpHeightCar { get; private set; }
        [field: SerializeField] public GameState InitialState { get; private set; }
    }
}
