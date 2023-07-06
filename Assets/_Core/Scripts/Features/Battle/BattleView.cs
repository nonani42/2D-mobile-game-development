using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Battle
{
    internal class BattleView : MonoBehaviour
    {
        [field: Header("Player Stats")]
        [field: SerializeField] public TMP_Text CountMoneyText { get; private set; }
        [field: SerializeField] public TMP_Text CountHealthText { get; private set; }
        [field: SerializeField] public TMP_Text CountPowerText { get; private set; }
        [field: SerializeField] public TMP_Text CountHostilityText { get; private set; }

        [field: Header("Enemy Stats")]
        [field: SerializeField] public TMP_Text CountPowerEnemyText { get; private set; }

        [field: Header("Money Buttons")]
        [field: SerializeField] public Button AddMoneyButton { get; private set; }
        [field: SerializeField] public Button SubtractMoneyButton { get; private set; }

        [field: Header("Health Buttons")]
        [field: SerializeField] public Button AddHealthButton { get; private set; }
        [field: SerializeField] public Button SubtractHealthButton { get; private set; }

        [field: Header("Power Buttons")]
        [field: SerializeField] public Button AddPowerButton { get; private set; }
        [field: SerializeField] public Button SubtractPowerButton { get; private set; }

        [field: Header("Hostility Buttons")]
        [field: SerializeField] public Button AddHostilityButton { get; private set; }
        [field: SerializeField] public Button SubtractHostilityButton { get; private set; }

        [field: Header("Other Buttons")]
        [field: SerializeField] public Button FightButton { get; private set; }
        [field: SerializeField] public Button AvoidButton { get; private set; }

        [field: Header("Result")]
        [field: SerializeField] public TMP_Text EncounterResultText { get; private set; }
        [field: SerializeField] public Button RetryButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }

        [field: Header("Configs")]
        [field: SerializeField] public EncounterResultConfigDataSource EncounterResults { get; private set; }
    }
}