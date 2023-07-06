using UnityEngine;

namespace Features.Battle
{
    [CreateAssetMenu(
    fileName = nameof(EncounterResultConfigDataSource),
    menuName = "Configs/" + "Battle/" + nameof(EncounterResultConfigDataSource))]

    internal class EncounterResultConfigDataSource : ScriptableObject
    {
        [field: SerializeField] public EncounterResultConfig[] EncounterResultConfigs { get; private set; }
    }
}
