using UnityEngine;
namespace _Game.Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip ambientSoundAudioClip;
        private static AudioManager instance;

        private void Start() {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            // audioSource.clip = amb
        }

        public static void PlayAudioClip(AudioClip audioClip) {
            if (instance) {
                instance.audioSource.PlayOneShot(audioClip);
            }
        }
    }
}