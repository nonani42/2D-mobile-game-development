using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _backBtn;

    public void Init(UnityAction backToMenu) => _backBtn.onClick.AddListener(backToMenu);

    public void OnDestroy() => _backBtn.onClick.RemoveAllListeners();
}
