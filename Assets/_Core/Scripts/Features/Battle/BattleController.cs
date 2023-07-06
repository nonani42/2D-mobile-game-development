using Profile;
using System;
using System.Linq;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Features.Battle
{
    internal class BattleController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Battle/BattleView");

        private readonly Transform _placeForUI;
        private readonly ProfilePlayer _profilePlayer;
        private readonly BattleView _view;


        private PlayerData _money;
        private PlayerData _health;
        private PlayerData _power;
        private PlayerData _hostility;

        private IEnemy _enemy;

        private IEncounterResult[] _encounterResultConfigs;


        public BattleController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUI = placeForUi;
            _profilePlayer = profilePlayer;

            _view = LoadView(placeForUi);

            _enemy = new Enemy("Enemy Truck");

            _money = CreatePlayerData(DataType.Money);
            _health = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _hostility = CreatePlayerData(DataType.Hostility);

            Subscribe();

            SetInitialValues();

            _encounterResultConfigs = _view.EncounterResults.EncounterResultConfigs;
        }

        private BattleView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BattleView>();
        }

        private void SetInitialValues()
        {
            SetUIVisibility(BattleStateType.Preparation);

            ChangeUiWindow(_money);
            ChangeUiWindow(_health);
            ChangeUiWindow(_power);
            ChangeUiWindow(_hostility);
        }

        protected override void OnDispose()
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
            }
            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }

        private void Subscribe()
        {
            _view.AddMoneyButton.onClick.AddListener(IncreaseMoney);
            _view.SubtractMoneyButton.onClick.AddListener(DecreaseMoney);

            _view.AddHealthButton.onClick.AddListener(IncreaseHealth);
            _view.SubtractHealthButton.onClick.AddListener(DecreaseHealth);

            _view.AddPowerButton.onClick.AddListener(IncreasePower);
            _view.SubtractPowerButton.onClick.AddListener(DecreasePower);

            _view.AddHostilityButton.onClick.AddListener(IncreaseHostility);
            _view.SubtractHostilityButton.onClick.AddListener(DecreaseHostility);

            _view.AvoidButton.onClick.AddListener(Avoid);
            _view.FightButton.onClick.AddListener(Fight);

            _view.RetryButton.onClick.AddListener(Retry);
            _view.ExitButton.onClick.AddListener(Close);
        }

        private void Unsubscribe()
        {
            _view.AddMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _view.SubtractMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _view.AddHealthButton.onClick.RemoveListener(IncreaseHealth);
            _view.SubtractHealthButton.onClick.RemoveListener(DecreaseHealth);

            _view.AddPowerButton.onClick.RemoveListener(IncreasePower);
            _view.SubtractPowerButton.onClick.RemoveListener(DecreasePower);

            _view.AddHostilityButton.onClick.RemoveListener(IncreaseHostility);
            _view.SubtractHostilityButton.onClick.RemoveListener(DecreaseHostility);

            _view.AvoidButton.onClick.RemoveListener(Avoid);
            _view.FightButton.onClick.RemoveListener(Fight);

            _view.RetryButton.onClick.RemoveListener(Retry);
            _view.ExitButton.onClick.RemoveListener(Close);
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
            ChangeUiWindow(playerData);
        }

        private void ChangeUiWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CalcPower();
            _view.CountPowerEnemyText.text = $"Enemy Power {enemyPower}";

            switch (dataType)
            {
                case DataType.Hostility:
                    float playerHostilityThreshold = 2;
                    _view.AvoidButton.gameObject.SetActive(!(value > playerHostilityThreshold));

                    SetDataBrackets(_view.AddHostilityButton, _view.SubtractHostilityButton, value, 0, 5);
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
            switch (dataType)
            {
                case DataType.Money: return _view.CountMoneyText;
                case DataType.Health: return _view.CountHealthText;
                case DataType.Power: return _view.CountPowerText;
                case DataType.Hostility: return _view.CountHostilityText;
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
            _view.EncounterResultText.text = message;

            SetUIVisibility(BattleStateType.Result);
        }

        private void Retry()
        {
            SetInitialValues();
        }

        private void SetUIVisibility(BattleStateType state)
        {
            bool res;
            switch (state)
            {
                case BattleStateType.Preparation:
                    res = true;
                    break;
                case BattleStateType.Result:
                    res = false;
                    break;
                default:
                    res = false;
                    break;
            }

            _view.RetryButton.gameObject.SetActive(!res);
            _view.ExitButton.gameObject.SetActive(!res);

            _view.EncounterResultText.enabled = !res;

            _view.AddMoneyButton.gameObject.SetActive(res);
            _view.SubtractMoneyButton.gameObject.SetActive(res);
            _view.AddHealthButton.gameObject.SetActive(res);
            _view.SubtractHealthButton.gameObject.SetActive(res);
            _view.AddPowerButton.gameObject.SetActive(res);
            _view.SubtractPowerButton.gameObject.SetActive(res);
            _view.AddHostilityButton.gameObject.SetActive(res);
            _view.SubtractHostilityButton.gameObject.SetActive(res);
            _view.FightButton.gameObject.SetActive(res);
            _view.AvoidButton.gameObject.SetActive(res);
        }

        private void Close() => _profilePlayer.CurrentState.Value = GameState.Game;
    }
}
