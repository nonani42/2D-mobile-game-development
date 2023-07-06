using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Battle
{
    internal class StartBattleView : MonoBehaviour
    {
        [SerializeField] private Button _startFightButton;

        private UnityAction _startFight;

        public void Init(UnityAction startFight)
        {
            _startFight = startFight;
            _startFightButton.onClick.AddListener(_startFight);
        }

        private void OnDestroy() =>
            _startFightButton.onClick.RemoveListener(_startFight);
    }
}
