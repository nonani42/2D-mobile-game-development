using System;
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

        [Header("Other Buttons")]
        [SerializeField] private Button _fightButton;

        private PlayerData _money;
        private PlayerData _health;
        private PlayerData _power;

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _health = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);

            Subscribe();

            SetInitialValues();
        }

        private void SetInitialValues()
        {
            ChangeDataWindow(_money);
            ChangeDataWindow(_health);
            ChangeDataWindow(_power);
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _health);
            DisposePlayerData(ref _power);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
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

            _fightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveAllListeners();
            _subtractMoneyButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _subtractHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _subtractPowerButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_health);
        private void DecreaseHealth() => DecreaseValue(_health);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

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
        }

        private TMP_Text GetTextComponent(DataType dataType)
        {
            switch(dataType)
            {
                case DataType.Money: return _countMoneyText;
                case DataType.Health: return _countHealthText;
                case DataType.Power: return _countPowerText;
                default: throw new ArgumentException($"Wrong {nameof(DataType)}");
            };
        }

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
    }
}