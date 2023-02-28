using UnityEngine.Events;

namespace CarGame
{
    internal interface IAdsService
    {
        IAdsPlayer InterstitialPlayer { get; }
        IAdsPlayer RewardedPlayer { get; }
        IAdsPlayer BannerPlayer { get; }
        UnityEvent Initialized { get; }
        bool IsInitialized { get; }
    }
}
