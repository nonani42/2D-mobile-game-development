using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Advertisements;

namespace CarGame
{
    internal sealed class RewardedPlayer : UnityAdsPlayer
    {
        public RewardedPlayer(string id) : base(id) { }

        protected override void OnPlaying() => Advertisement.Show(Id, this);
        protected override void Load() => Advertisement.Load(Id, this);
    }
}
