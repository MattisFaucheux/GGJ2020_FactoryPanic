using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource), typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector] public static AudioManager instance;
        [Header("FX AudioSources")]
        public AudioSource m_introAudioSource;
        public AudioSource m_loopAudioSource;
        public AudioSource m_OneShotAudioSource;
        public AudioSource m_EngineAudioSource;
        [Header("Music AudioSources")]
        public AudioSource m_AmbientMusicAudioSource;

        [Header("FX AudioClips")] [Header("Music AudioClips")]
        public AudioClip AmbientMusicClip;

        private bool m_isMusicPlaying;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void SceneLoaded(Scene p_arg0, LoadSceneMode p_arg1)
        {
            if (p_arg0.buildIndex != 0)
            {
                if (m_EngineAudioSource == null || m_introAudioSource == null || m_loopAudioSource == null || m_OneShotAudioSource == null ||
                    SceneManager.GetActiveScene().buildIndex == 0)
                {
                    return;
                }
                UpdateMusic();
            }
        }

        private void Update()
        {
            if (m_EngineAudioSource == null || m_introAudioSource == null || m_loopAudioSource == null || m_OneShotAudioSource == null ||
                SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }
            UpdateMusic();
            
        }

        private void UpdateMusic()
        {
            if (m_isMusicPlaying == false)
            {
                m_isMusicPlaying = true;
                m_AmbientMusicAudioSource.loop = true;
                m_AmbientMusicAudioSource   .clip = AmbientMusicClip;
                m_AmbientMusicAudioSource.Play();
            }
        }

        private void ResetAudioSource(AudioSource p_as)
        {
            p_as.Stop();
            p_as.clip = null;
            p_as.loop = false;
        }

        public void PlaySoundOnce(AudioClip p_audioClip)
        {
            m_OneShotAudioSource.PlayOneShot(p_audioClip);
        }

        public void SetVolume (AudioSource p_audioSource, float p_volume)
        {
            p_audioSource.volume = p_volume;
        }
    }
}