using System;
using UnityEngine;

namespace CarGame
{
    [Serializable]
    internal class AdsPlayerSettings
    {
        [field: SerializeField] public bool Enabled { get; private set; }
        [SerializeField] private string _androidId;
        [SerializeField] private string _iosId;

        public string Id
        {
            get
            {
#if UNITY_EDITOR
                return _androidId;
#else
                switch (Application.platform)
                {
                    case (RuntimePlatform.Android):
                        return _androidId;
                    case (RuntimePlatform.IPhonePlayer):
                        return _iosId;
                    default:
                        return "";
                }
#endif
            }
        }
    }
}
