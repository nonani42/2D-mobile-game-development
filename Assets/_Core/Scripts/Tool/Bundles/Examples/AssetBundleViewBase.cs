using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Tool.Bundles.Examples
{
    internal class AssetBundleViewBase : MonoBehaviour
    {
        private const string UrlAssetSpritesBundle = "https://drive.google.com/uc?export=download&id=1t8FLidcXidYejcdYrMDXbfYIpvkExcdz";
        private const string UrlAssetAudioBundle = "https://drive.google.com/uc?export=download&id=1GEJSUhOdQ-xRdHt5-8qu0ioIJF2DUXqw";
        private const string UrlAssetButtonImageBundle = "https://drive.google.com/uc?export=download&id=1aszVQ7KApyIpi6Hdu9ie_y0looBwM7XX";
        
        [Header("Asset Bundles Targets")]
        [SerializeField] private DataSpriteBundle[] _spriteDataBundles;
        [SerializeField] private DataAudioBundle[] _audioDataBundles;
        [SerializeField] private DataSpriteBundle[] _buttonImageDataBundles;

        private AssetBundle _spritesAssetBundle;
        private AssetBundle _audioAssetBundle;
        private AssetBundle _buttonImageAssetBundle;

        protected IEnumerator ImplementSpriteBundles()
        {
            yield return DownloadSpriteAssetsBundle();

            if (_spritesAssetBundle != null)
                SetSpriteAssets(_spritesAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_spritesAssetBundle)} failed to load");
        }

        protected IEnumerator ImplementAudioBundles()
        {
            yield return DownloadAudioAssetsBundle();

            if (_audioAssetBundle != null)
                SetAudioAssets(_audioAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_audioAssetBundle)} failed to load");
        }

        protected IEnumerator ImplementButtonImageBundles()
        {
            yield return DownloadButtonImageAssetsBundle();

            if (_buttonImageAssetBundle != null)
                SetButtonImageAssets(_buttonImageAssetBundle);
            else
                Debug.LogError($"AssetBundle {nameof(_buttonImageAssetBundle)} failed to load");
        }

        private IEnumerator DownloadSpriteAssetsBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetSpritesBundle);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _spritesAssetBundle);
        }

        private IEnumerator DownloadAudioAssetsBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetAudioBundle);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _audioAssetBundle);
        }

        private IEnumerator DownloadButtonImageAssetsBundle()
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(UrlAssetButtonImageBundle);

            yield return request.SendWebRequest();

            while (!request.isDone)
                yield return null;

            StateRequest(request, out _buttonImageAssetBundle);
        }

        private void StateRequest(UnityWebRequest request, out AssetBundle assetBundle)
        {
            if (request.error == null)
            {
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
                Debug.Log("Complete");
            }
            else
            {
                assetBundle = null;
                Debug.LogError(request.error);
            }
        }

        private void SetSpriteAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _spriteDataBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }

        private void SetAudioAssets(AssetBundle assetBundle)
        {
            foreach (DataAudioBundle data in _audioDataBundles)
            {
                data.AudioSource.clip = assetBundle.LoadAsset<AudioClip>(data.NameAssetBundle);
                data.AudioSource.Play();
            }
        }

        private void SetButtonImageAssets(AssetBundle assetBundle)
        {
            foreach (DataSpriteBundle data in _buttonImageDataBundles)
                data.Image.sprite = assetBundle.LoadAsset<Sprite>(data.NameAssetBundle);
        }
    }
}
