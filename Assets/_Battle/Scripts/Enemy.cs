using UnityEngine;

namespace BattleScripts
{
    internal interface IEnemy
    {
        void Update(PlayerData playerData);

        int CalcPower();
    }

    internal class Enemy : IEnemy
    {
        private const float KMoney = 5f;
        private const float KPower = 1f;
        private const float MaxHealthPlayer = 20;

        private readonly string _name;

        private int _playerMoney;
        private int _playerHealth;
        private int _playerPower;


        public Enemy(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Money:
                    _playerMoney = playerData.Value;
                    break;

                case DataType.Health:
                    _playerHealth = playerData.Value;
                    break;

                case DataType.Power:
                    _playerPower = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int kHealth = CalcKHealth();
            float moneyRatio = _playerMoney / KMoney;
            float powerRatio = _playerPower / KPower;

            return (int)(moneyRatio + kHealth + powerRatio);
        }

        private int CalcKHealth() =>
            _playerHealth > MaxHealthPlayer ? 25 : 10;
    }
}