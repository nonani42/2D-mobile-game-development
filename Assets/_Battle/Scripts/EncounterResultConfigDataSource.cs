using UnityEngine;

namespace BattleScripts
{
    [CreateAssetMenu(
    fileName = nameof(EncounterResultConfigDataSource),
    menuName = "Configs/" + nameof(EncounterResultConfigDataSource))]

    internal class EncounterResultConfigDataSource : ScriptableObject
    {
        [field: SerializeField] public EncounterResultConfig[] EncounterResultConfigs { get; private set; }
    }
}
