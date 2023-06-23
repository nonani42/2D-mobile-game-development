using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countHostilityText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _subtractMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _subtractHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _subtractPowerButton;

        [Header("Hostility Buttons")]
        [SerializeField] private Button _addHostilityButton;
        [SerializeField] private Button _subtractHostilityButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _avoidButton;

        [Header("Result")]
        [SerializeField] private TMP_Text _encounterResultText;
        [SerializeField] private Button _retryButton;

        [Header("Configs")]
        [SerializeField] private EncounterResultConfigDataSource _encounterResults;


        private PlayerData _money;
        private PlayerData _health;
        private PlayerData _power;
        private PlayerData _hostility;

        private IEnemy _enemy;
        private EncounterUI _encounterUi;

        private IEncounterResult[] _encounterResultConfigs;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");
            _encounterUi = new EncounterUI("Avoid Button");

            _money = CreatePlayerData(DataType.Money);
            _health = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _hostility = CreatePlayerData(DataType.Hostility);

            Subscribe();

            SetInitialValues();

            _encounterResultConfigs = _encounterResults.EncounterResultConfigs;
        }

        private void SetInitialValues()
        {
            SetUIVisibility(GameStateType.Game);

            ChangeDataWindow(_money);
            ChangeDataWindow(_health);
            ChangeDataWindow(_power);
            ChangeDataWindow(_hostility);
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _health);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _hostility);

            Unsubscribe();
        }

        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            switch (dataType)
            {
                case DataType.Health:
                    playerData.Attach(_enemy);
                    break;
                case DataType.Money:
                    playerData.Attach(_enemy);
                    break;
                case DataType.Power:
                    playerData.Attach(_enemy);
                    break;
                case DataType.Hostility:
                    playerData.Attach(_encounterUi);
                    break;
            }
            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData.Detach(_encounterUi);
            playerData = null;
        }

        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _subtractMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _subtractHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _subtractPowerButton.onClick.AddListener(DecreasePower);

            _addHostilityButton.onClick.AddListener(IncreaseHostility);
            _subtractHostilityButton.onClick.AddListener(DecreaseHostility);

            _avoidButton.onClick.AddListener(Avoid);
            _fightButton.onClick.AddListener(Fight);

            _retryButton.onClick.AddListener(Retry);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _subtractMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _subtractHealthButton.onClick.RemoveListener(DecreaseHealth);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _subtractPowerButton.onClick.RemoveListener(DecreasePower);

            _addHostilityButton.onClick.RemoveListener(IncreaseHostility);
            _subtractHostilityButton.onClick.RemoveListener(DecreaseHostility);

            _avoidButton.onClick.RemoveListener(Avoid);
            _fightButton.onClick.RemoveListener(Fight);

            _retryButton.onClick.RemoveListener(Retry);
        }

        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_health);
        private void DecreaseHealth() => DecreaseValue(_health);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseHostility() => IncreaseValue(_hostility);
        private void DecreaseHostility() => DecreaseValue(_hostility);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";

            switch (dataType)
            {
                case DataType.Hostility:
                    bool isHostile = _encounterUi.CheckHostility();
                    _avoidButton.gameObject.SetActive(!isHostile);

                    SetDataBrackets(_addHostilityButton, _subtractHostilityButton, value, 0, 5);
                    break;
            }
        }

        private void SetDataBrackets(Button addBtn, Button subtractBtn, int value, int leftBracket, int rightBracket)
        {
            addBtn.interactable = true;
            subtractBtn.interactable = true;

            if (value == rightBracket)
            {
                addBtn.interactable = false;
            }
            else if (value == leftBracket)
            {
                subtractBtn.interactable = false;
            }
        }

        private TMP_Text GetTextComponent(DataType dataType)
        {
            switch(dataType)
            {
                case DataType.Money: return _countMoneyText;
                case DataType.Health: return _countHealthText;
                case DataType.Power: return _countPowerText;
                case DataType.Hostility: return _countHostilityText;
                default: throw new ArgumentException($"Wrong {nameof(DataType)}");
            };
        }

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            int powerDifference = _power.Value - enemyPower;

            EncounterResultType resultType = EncounterResultType.Tie;

            if (powerDifference > 0)
                resultType = EncounterResultType.Win;
            else if (powerDifference < 0)
                resultType = EncounterResultType.Lose;

            IEncounterResult fightResult = _encounterResultConfigs.Where(item => item.Type == resultType).FirstOrDefault();

            ShowEncounterResult(fightResult);
        }

        private void Avoid()
        {
            IEncounterResult avoidResult = _encounterResultConfigs.Where(item => item.Type == EncounterResultType.Avoid).FirstOrDefault();

            ShowEncounterResult(avoidResult);
        }

        private void ShowEncounterResult(IEncounterResult result)
        {
            string color = "#" + ColorUtility.ToHtmlStringRGBA(result.Colour);
            string message = $"<color={color}>{result.Message}</color>";
            Debug.Log(message);
            _encounterResultText.text = message;

            SetUIVisibility(GameStateType.Finish);
        }

        private void Retry()
        {
            SetInitialValues();
        }

        private void SetUIVisibility(GameStateType state)
        {
            bool res;
            switch (state)
            {
                case GameStateType.Game:
                    res = true;
                    break;
                case GameStateType.Finish:
                    res = false;
                    break;
                default: 
                    res = false;
                    break;
            }

            _retryButton.gameObject.SetActive(!res);
            _encounterResultText.enabled = !res;

            _addMoneyButton.gameObject.SetActive(res);
            _subtractMoneyButton.gameObject.SetActive(res);
            _addHealthButton.gameObject.SetActive(res);
            _subtractHealthButton.gameObject.SetActive(res);
            _addPowerButton.gameObject.SetActive(res);
            _subtractPowerButton.gameObject.SetActive(res);
            _addHostilityButton.gameObject.SetActive(res);
            _subtractHostilityButton.gameObject.SetActive(res);
            _fightButton.gameObject.SetActive(res);
            _avoidButton.gameObject.SetActive(res);
        }
    }
}