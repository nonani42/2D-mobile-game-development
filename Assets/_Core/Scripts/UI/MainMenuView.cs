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



        public void Init(UnityAction startGame, UnityAction settings, UnityAction rewardedAd, UnityAction<string> buy)
        {
            _startBtn.onClick.AddListener(startGame);
            _settingsBtn.onClick.AddListener(settings);
            _rewardBtn.onClick.AddListener(rewardedAd);
            _buyBtn.onClick.AddListener(() => buy(_productId));
        }

        public void OnDestroy()
        {
            _startBtn.onClick.RemoveAllListeners();
            _settingsBtn.onClick.RemoveAllListeners();
            _buyBtn.onClick.RemoveAllListeners();
            _rewardBtn.onClick.RemoveAllListeners();
        }
    }
}
