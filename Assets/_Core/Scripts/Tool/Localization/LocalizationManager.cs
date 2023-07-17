using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Localization
{
    class LocalizationManager : LocalizationWindow
    {
        [Header("Localization Table")]
        [SerializeField] private string _tableName;
        [SerializeField] private string _assetTableName;

        [Header("Localization structures")]
        [SerializeField] private LocalizationStructure[] _structures;


        protected override void OnStarted()
        {
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLocaleChanged;
            OnSelectedLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        protected override void OnDestroyed()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLocaleChanged;
        }


        private void OnSelectedLocaleChanged(Locale _)
        {
            UpdateTextAsync();
            UpdateAssetAsync();
        }

        private void UpdateTextAsync()
        {
            LocalizationSettings.StringDatabase.GetTableAsync(_tableName).Completed +=
                handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        StringTable table = handle.Result;
                        foreach(var structure in _structures)
                            structure.changeText.text = table.GetEntry(structure.localizationTag)?.GetLocalizedString();
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {handle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
        }

        private void UpdateAssetAsync()
        {
            LocalizationSettings.AssetDatabase.GetTableAsync(_assetTableName).Completed +=
                assetTablehandle =>
                {
                    if (assetTablehandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        AssetTable table = assetTablehandle.Result;
                        foreach (var structure in _structures)
                        {
                            table.GetAssetAsync<Sprite>(structure.localizationTag).Completed +=
                            assetHandle =>
                            {
                                if (assetHandle.Status == AsyncOperationStatus.Succeeded)
                                    structure.changeImage.sprite = assetHandle.Result;
                                else
                                {
                                    string errorMessage = $"[{GetType().Name}] Could not load Asset Table: {assetHandle.OperationException}";
                                    Debug.LogError(errorMessage);
                                }
                            };
                        }
                    }
                    else
                    {
                        string errorMessage = $"[{GetType().Name}] Could not load String Table: {assetTablehandle.OperationException}";
                        Debug.LogError(errorMessage);
                    }
                };
        }
    }
}
