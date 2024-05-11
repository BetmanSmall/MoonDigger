using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace _Game.Scripts {
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour {
        [SerializeField] private AudioSource audioSourceMusic;
        [SerializeField] private AudioSource audioSourceSfx;
        [SerializeField] private Slider sliderMusic;
        [SerializeField] private Slider sliderSfx;
        // [SerializeField] private AudioClip ambientSoundAudioClip;
        [SerializeField] private float masterVolume = 1f;
        [SerializeField] private float musicVolume = 1f;
        [SerializeField] private float sfxVolume = 1f;
        [SerializeField] private List<AudioClip> digsAudioClips;
        [SerializeField] private List<AudioClip> gemsAudioClips;

        private static AudioManager instance;
        private void Start() {
            instance = this;
            // audioSource = GetComponent<AudioSource>(); 
            // audioSource.clip = amb
            MusicVolumeChanged(PlayerPrefs.GetFloat("musicVolume", 1f));
            sliderMusic.value = musicVolume;
            SfxVolumeChanged(PlayerPrefs.GetFloat("sfxVolume", 1f));
            sliderSfx.value = sfxVolume;
        }

        public static void PlayAudioClip(AudioClip audioClip) {
            if (instance) {
                instance.audioSourceSfx.PlayOneShot(audioClip);
            }
        }

        public static void PlayRandomDigs() {
            if (instance) {
                int index = Random.Range(0, instance.digsAudioClips.Count);
                instance.audioSourceSfx.PlayOneShot(instance.digsAudioClips[index]);
            }
        }

        public static void PlayRandomGems() {
            if (instance) {
                int index = Random.Range(0, instance.gemsAudioClips.Count);
                instance.audioSourceSfx.PlayOneShot(instance.gemsAudioClips[index]);
            }
        }

        // public void MasterVolumeChanged(float volume) {
        //     masterVolume = volume;
        //     audioSource.volume = masterVolume;
        //     PlayerPrefs.SetFloat("masterVolume", masterVolume);
        // }

        public void MusicVolumeChanged(float volume) {
            musicVolume = volume;
            audioSourceMusic.volume = musicVolume;
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }

        public void SfxVolumeChanged(float volume) {
            sfxVolume = volume;
            audioSourceSfx.volume = sfxVolume;
            PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        }
    }
}