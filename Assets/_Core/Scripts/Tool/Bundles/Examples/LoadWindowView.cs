using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using TMPro;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;
        [SerializeField] private Button _loadImageButton;

        [Header("Addressables")]
        [SerializeField] private AssetReference _spawningButtonPrefab;
        [SerializeField] private RectTransform _spawnedButtonsContainer;
        [SerializeField] private Button _spawnAssetButton;

        [SerializeField] private AssetReference _backgroundSprite;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;
        [SerializeField] private Image _backgroundComponent;


        private readonly List<AsyncOperationHandle<GameObject>> _addressablePrefabs =
            new List<AsyncOperationHandle<GameObject>>();

        private AsyncOperationHandle<Sprite> _addressableBackground;



        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
            _loadImageButton.onClick.AddListener(LoadButtonImage);

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
            StartCoroutine(DownloadAssetBundles());
        }

        private void LoadButtonImage()
        {
            _loadImageButton.interactable = false;
            StartCoroutine(DownloadButtonImageBundles());
        }
        #endregion

        #region Adressables
        private void AddBackground()
        {
            _addressableBackground = Addressables.LoadAssetAsync<Sprite>(_backgroundSprite);
            _addressableBackground.Completed += SetBackground;
        }

        private void SetBackground(AsyncOperationHandle<Sprite> adressable)
        {
            _backgroundComponent.sprite = adressable.Result;
        }

        [ContextMenu(nameof(RemoveBackground))]
        private void RemoveBackground()
        {
            if (!_addressableBackground.IsValid())
                return;
            _addressableBackground.Completed -= SetBackground;
            Addressables.Release(_addressableBackground);
            _backgroundComponent.sprite = null;
        }

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
