using UnityEngine;

namespace Tool
{
    class SoundManager : MonoBehaviour
    {
        internal static SoundManager instance { get; private set; }

        [field: Header("Audio Sources")]
        [field: SerializeField] public AudioSource SoundSource { get; private set; }
        [field: SerializeField] public AudioSource MusicSource { get; private set; }

        [field: Header("Clips")]
        [field: SerializeField] public AudioClip ButtonSound { get; private set; }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
