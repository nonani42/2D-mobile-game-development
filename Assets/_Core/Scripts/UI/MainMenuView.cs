using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _settingsBtn;

    public void Init(UnityAction startGame, UnityAction settings) 
    { 
        _startBtn.onClick.AddListener(startGame);
        _settingsBtn.onClick.AddListener(settings);
    }

    public void OnDestroy()
    { 
        _startBtn.onClick.RemoveAllListeners();
        _settingsBtn.onClick.RemoveAllListeners();
    }
}
