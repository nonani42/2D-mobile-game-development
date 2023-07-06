using UnityEngine;
using UnityEngine.UI;
using Tool;

namespace Tool.Tween
{
    [RequireComponent(typeof(Button))]
    internal class AudioButtonComponent : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private SoundManager _manager;

        private void Awake() => InitComponents();

        private void Start() => _button.onClick.AddListener(OnButtonClick);
        private void OnDestroy() => _button.onClick.RemoveListener(OnButtonClick);

        private void InitComponents()
        {
            _manager = SoundManager.instance;
            if (_button == null)
                _button = GetComponent<Button>();
            if (_audioSource == null)
                _audioSource = _manager.SoundSource;
            if (_audioClip == null)
                _audioClip = _manager.ButtonSound;
        }


        private void OnButtonClick() => ActivateSound();
        private void ActivateSound() => _audioSource.PlayOneShot(_audioClip);
    }
}
