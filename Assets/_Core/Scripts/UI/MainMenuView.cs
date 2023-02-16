using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startBtn;

    public void Init(UnityAction startGame) => _startBtn.onClick.AddListener(startGame);

    public void OnDestroy() => _startBtn.onClick.RemoveAllListeners();
}
