using UnityEngine;

namespace Features.Battle
{
    internal interface IEncounterResult
    {
        EncounterResultType Type { get; }
        string Message { get; }
        Color Colour { get; }
    }

    [CreateAssetMenu(fileName = nameof(EncounterResultConfig), menuName = "Configs/" + "Battle/" + nameof(EncounterResultConfig))]
    internal class EncounterResultConfig : ScriptableObject, IEncounterResult
    {
        [field: SerializeField] public EncounterResultType Type { get; private set; }
        [field: SerializeField] public string Message { get; private set; }
        [field: SerializeField] public Color Colour { get; private set; }
    }
}