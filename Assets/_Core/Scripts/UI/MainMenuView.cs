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




        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAd, UnityAction<string> buy, UnityAction shed)
        {
            _startBtn.onClick.AddListener(startGame);
            _settingsBtn.onClick.AddListener(settings);
            _rewardBtn.onClick.AddListener(rewardedAd);
            _buyBtn.onClick.AddListener(() => buy(_productId));
            _shedBtn.onClick.AddListener(shed);
        }

        public void OnDestroy()
        {
            _startBtn.onClick.RemoveAllListeners();
            _settingsBtn.onClick.RemoveAllListeners();
            _buyBtn.onClick.RemoveAllListeners();
            _rewardBtn.onClick.RemoveAllListeners();
            _shedBtn.onClick.RemoveAllListeners();
        }
    }
}
