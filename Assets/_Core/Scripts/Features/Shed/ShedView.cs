using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.Shed
{
    internal interface IShedView
    {
        void Init(UnityAction apply);
        void Deinit();
    }

    internal class ShedView : MonoBehaviour, IShedView
    {
        [SerializeField] private Button _buttonApply;


        private void OnDestroy() => Deinit();

        public void Init(UnityAction apply)
        {
            _buttonApply.onClick.AddListener(apply);
        }

        public void Deinit()
        {
            _buttonApply.onClick.RemoveAllListeners();
        }
    }
}