using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

namespace CarGame
{
    internal class UnityAdsService : MonoBehaviour, IUnityAdsInitializationListener, IAdsService
    {
        public static UnityAdsService instance { get; private set; }

        [Header("Components")]
        [SerializeField] private UnityAdsSettings _settings;

        [field: Header("Events")]
        [field: SerializeField] public UnityEvent Initialized { get; private set; }

        public IAdsPlayer InterstitialPlayer { get; private set; }
        public IAdsPlayer RewardedPlayer { get; private set; }
        public IAdsPlayer BannerPlayer { get; private set; }
        public bool IsInitialized => Advertisement.isInitialized;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            InitializeAds();
            InitializePlayers();
        }

        private void InitializeAds() =>
            Advertisement.Initialize(
                _settings.GameId,
                _settings.TestMode,
                this);

        private void InitializePlayers()
        {
            InterstitialPlayer = CreateInterstitial();
            RewardedPlayer = CreateRewarded();
            BannerPlayer = CreateBanner();
        }


        private IAdsPlayer CreateInterstitial() =>
            _settings.Interstitial.Enabled
                ? (IAdsPlayer)new InterstitialPlayer(_settings.Interstitial.Id)
                : (IAdsPlayer)new StubPlayer("null");

        private IAdsPlayer CreateRewarded() => _settings.Rewarded.Enabled
                ? (IAdsPlayer)new RewardedPlayer(_settings.Rewarded.Id)
                : (IAdsPlayer)new StubPlayer("null");

        private IAdsPlayer CreateBanner() =>
            new StubPlayer("null");


        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            Log("Initialization complete.");
            Initialized?.Invoke();
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message) =>
            Error($"Initialization Failed: {error.ToString()} - {message}");


        private void Log(string message) => message.ToString(); //Debug.Log(WrapMessage(message));
        private void Error(string message) => message.ToString(); //Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";
    }
}
