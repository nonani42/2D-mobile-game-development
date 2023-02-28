using UnityEngine.Advertisements;

namespace CarGame
{
    internal sealed class InterstitialPlayer : UnityAdsPlayer
    {
        public InterstitialPlayer(string id) : base(id) { }

        protected override void OnPlaying() => Advertisement.Show(Id, this);
        protected override void Load() => Advertisement.Load(Id, this);
    }
}
