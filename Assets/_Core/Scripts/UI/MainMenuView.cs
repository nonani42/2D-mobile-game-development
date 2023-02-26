using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _settingsBtn;

    public void Init(UnityAction startGame) => _startBtn.onClick.AddListener(startGame);
    public void InitSettings(UnityAction settingsMenu) => _settingsBtn.onClick.AddListener(settingsMenu);

    public void OnDestroy()
    { 
        _startBtn.onClick.RemoveAllListeners();
        _settingsBtn.onClick.RemoveAllListeners();
    }
}
