using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CarGame
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        public event Action Started;
        public event Action Finished;
        public event Action Failed;
        public event Action Skipped;
        public event Action BecomeReady;

        protected readonly string Id;


        protected UnityAdsPlayer(string id)
        {
            Id = id;
        }


        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void OnPlaying();
        protected abstract void Load();


        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Ready");
            BecomeReady?.Invoke();
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Error($"Ad failed to load {adUnitId} with {error}");
        }

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) =>
            Error($"Error: {message} with {error}");

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (IsIdMy(placementId) == false)
                return;

            Log("Started");
            Started?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string adUnitId)
        {
            Log($"Ad clicked {adUnitId}");
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showResult)
        {
            if (IsIdMy(placementId) == false)
                return;

            switch (showResult)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    Log("Finished");
                    Finished?.Invoke();
                    break;

                case UnityAdsShowCompletionState.UNKNOWN:
                    Error("Failed");
                    Failed?.Invoke();
                    break;

                case UnityAdsShowCompletionState.SKIPPED:
                    Log("Skipped");
                    Skipped?.Invoke();
                    break;
            }
        }


        private bool IsIdMy(string id) => Id == id;

        private void Log(string message) => message.ToString(); //Debug.Log(WrapMessage(message));
        private void Error(string message) => message.ToString(); //Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";

    }
}
