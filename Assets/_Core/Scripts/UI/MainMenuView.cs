using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CarGame
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private string _productId;

        [SerializeField] private Button _startBtn;
        [SerializeField] private Button _settingsBtn;
        [SerializeField] private Button _buyBtn;
        [SerializeField] private Button _rewardBtn;
        [SerializeField] private Button _shedBtn;
        [SerializeField] private Button _dailyRewardBtn;
        [SerializeField] private Button _exitBtn;


        UnityAction _startGame;
        UnityAction _settings;
        UnityAction _rewardedAd;
        UnityAction<string> _buy;
        UnityAction _shed;
        UnityAction _dailyReward;
        UnityAction _exit;


        public void Init(
            UnityAction startGame, 
            UnityAction settings, 
            UnityAction rewardedAd, 
            UnityAction<string> buy, 
            UnityAction shed, 
            UnityAction dailyReward, 
            UnityAction exit
            )
        {
            _startGame = startGame;
            _settings = settings;
            _rewardedAd = rewardedAd;
            _buy = buy;
            _shed = shed;
            _dailyReward = dailyReward;
            _exit = exit;

            _startBtn.onClick.AddListener(_startGame);
            _settingsBtn.onClick.AddListener(_settings);
            _rewardBtn.onClick.AddListener(_rewardedAd);
            _buyBtn.onClick.AddListener(() => _buy(_productId));
            _shedBtn.onClick.AddListener(_shed);
            _dailyRewardBtn.onClick.AddListener(_dailyReward);
            _exitBtn.onClick.AddListener(_exit);
        }

        public void OnDestroy()
        {
            _startBtn.onClick.RemoveListener(_startGame);
            _settingsBtn.onClick.RemoveListener(_settings);
            _rewardBtn.onClick.RemoveListener(_rewardedAd);
            _buyBtn.onClick.RemoveListener(() => _buy(_productId));
            _shedBtn.onClick.RemoveListener(_shed);
            _dailyRewardBtn.onClick.RemoveListener(_dailyReward);
            _exitBtn.onClick.RemoveListener(_exit);
        }
    }
}
