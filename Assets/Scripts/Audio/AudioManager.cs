using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
namespace GameOne {
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public AudioFile[] audioFiles;

        void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            foreach (var s in audioFiles) {
                s.audioSource = gameObject.AddComponent<AudioSource>();
                s.audioSource.clip = s.audioClip;
                s.audioSource.volume = s.volume;
                s.audioSource.loop = s.isLooping;

                if (s.playOnAwake) { s.audioSource.Play(); }
            }

        }

        public static void PlayMusic(string name) {
            AudioFile audio = obterAudioPorNome(name);
            if (audio == null) {
                Debug.Log("Audio " + name + " not found!");
            } else {
                audio.audioSource.Play();
            }
        }

        public static void StopMusic(string name) {
            AudioFile audio = obterAudioPorNome(name);
            if (audio == null) {
                Debug.Log("Audio " + name + " not found!");
            } else {
                audio.audioSource.Stop();
            }
        }

        public static void PauseMusic(string name) {
            AudioFile audio = obterAudioPorNome(name);
            if (audio == null) {
                Debug.Log("Audio " + name + " not found!");
            } else {
                audio.audioSource.Pause();
            }
        }

        public static void UnPauseMusic(string name) {
            AudioFile audio = obterAudioPorNome(name);
            if (audio == null) {
                Debug.Log("Audio " + name + " not found!");
            } else {
                audio.audioSource.UnPause();
            }
        }

        private static AudioFile obterAudioPorNome(string name) {
            return Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName.Equals(name));
        }
    }
}