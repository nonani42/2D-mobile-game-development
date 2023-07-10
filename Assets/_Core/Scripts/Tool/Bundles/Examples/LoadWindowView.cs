using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles Buttons")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _loadImageButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [Header("Addressable Background")]
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;
        [SerializeField] private Image _backgroundComponent;
        [SerializeField] private AssetReference _backgroundSprite;


        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _addressableBackground;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _loadImageButton.onClick.AddListener(LoadButtonBackground);

            _spawnAssetButton.onClick.AddListener(SpawnButtons);

            _addBackgroundButton.onClick.AddListener(AddBackground);
            _removeBackgroundButton.onClick.AddListener(RemoveBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _spawnAssetButton.onClick.RemoveAllListeners();

            _loadImageButton.onClick.RemoveAllListeners();

            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();

            DespawnButtons();
            RemoveBackground();
        }

        #region AssetBundle
        private void LoadAssets()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(ImplementSpriteBundles());
            StartCoroutine(ImplementAudioBundles());
        }

        private void LoadButtonBackground()
        {
            _loadImageButton.interactable = false;
            StartCoroutine(ImplementButtonImageBundles());
        }
        #endregion

        #region Adressables
        private void AddBackground()
        {
            if (!_addressableBackground.IsValid())
            {
                _addressableBackground = Addressables.LoadAssetAsync<Sprite>(_backgroundSprite);
                _addressableBackground.Completed += OnBackgroundLoaded;
            }
        }

        [ContextMenu(nameof(RemoveBackground))]
        private void RemoveBackground()
        {
            if (_addressableBackground.IsValid())
            {
                _addressableBackground.Completed -= OnBackgroundLoaded;
                Addressables.Release(_addressableBackground);

                SetBackground(null);
            }
        }

        private void OnBackgroundLoaded(AsyncOperationHandle<Sprite> asyncOperationHandle)
        {
            asyncOperationHandle.Completed -= OnBackgroundLoaded;
            SetBackground(asyncOperationHandle.Result);
        }

        private void SetBackground(Sprite sprite) => _backgroundComponent.sprite = sprite;

        private void SpawnButtons()
        {
            AsyncOperationHandle<GameObject> addressablePrefab =
                Addressables.InstantiateAsync(_spawningButtonPrefab, _spawnedButtonsContainer);

            _addressablePrefabs.Add(addressablePrefab);
        }

        [ContextMenu(nameof(DespawnButtons))]
        private void DespawnButtons()
        {
            if (_addressablePrefabs == null)
                return;
            foreach (AsyncOperationHandle<GameObject> addressablePrefab in _addressablePrefabs)
                Addressables.ReleaseInstance(addressablePrefab);

            _addressablePrefabs.Clear();
        }
        #endregion
    }
}
