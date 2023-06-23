using UnityEngine;

namespace BattleScripts
{
    internal interface IEncounterUI : IPlayerDataObserver
    {
    }

    internal class EncounterUI : IEncounterUI
    {
        private const float PlayerHostilityThreshold = 2;

        private readonly string _name;

        private int _playerHostility;


        public EncounterUI(string name) =>
            _name = name;


        public void Update(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Hostility:
                    _playerHostility = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public bool CheckHostility()
        {
            return _playerHostility > PlayerHostilityThreshold;
        }
    }
}