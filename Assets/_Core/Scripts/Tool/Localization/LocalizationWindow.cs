using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

namespace Tool.Localization
{
    internal abstract class LocalizationWindow : MonoBehaviour
    {
        [Header("Scene Components")]
        [SerializeField] private TMP_Dropdown _changeLanguage;


        private void Start()
        {
            _changeLanguage.onValueChanged.AddListener(ChangeLanguage);
            OnStarted();
        }

        private void OnDestroy()
        {
            _changeLanguage.onValueChanged.RemoveAllListeners();
            OnDestroyed();
        }


        protected virtual void OnStarted() { }
        protected virtual void OnDestroyed() { }


        private void ChangeLanguage(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }
    }
}
