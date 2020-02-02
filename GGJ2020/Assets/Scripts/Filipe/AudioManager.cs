using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Managers
{
    [RequireComponent(typeof(AudioSource), typeof(AudioSource), typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector] public static AudioManager instance;
        [FormerlySerializedAs("m_introAudioSource")] [Header("FX AudioSources")]
        public AudioSource m_generatorAudioSource;
        public AudioSource m_loopAudioSource;
        public AudioSource m_OneShotAudioSource;
        [FormerlySerializedAs("m_EngineAudioSource")] 
        public AudioSource m_PipeAudioSource;
        [Header("Music AudioSources")]
        public AudioSource m_AmbientMusicAudioSource;

        [Header("Music AudioClips")]
        public AudioClip AmbientMusicClip;
        [Header("FX AudioClips")]
        public List<AudioClip> m_pipeGasClips;
        public List<AudioClip> m_mopClips;
        public List<AudioClip> m_oilClips;
        public List<AudioClip> m_switchesClips;
        public List<AudioClip> m_conveyorClips;
        public List<AudioClip> m_buttonClips;
        public AudioClip m_generatorClip;
 
        private bool m_isOnFire;
        private bool m_isGenPlaying;
        private bool m_isLeaking;
        private bool m_isLeakPlaying;
        private bool m_isSwitching;
        private bool m_isOilSpawning;
        private bool m_isConveyorMoving;
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
                if (m_PipeAudioSource == null || m_generatorAudioSource == null || m_loopAudioSource == null || m_OneShotAudioSource == null ||
                    SceneManager.GetActiveScene().buildIndex == 0)
                {
                    return;
                }
                UpdateMusic();
            }
        }

        private void Update()
        {
            if (m_PipeAudioSource == null || m_generatorAudioSource == null || m_loopAudioSource == null || m_OneShotAudioSource == null ||
                SceneManager.GetActiveScene().buildIndex == 0)
            {
                return;
            }
            UpdateMusic();
            UpdateGenerator();
            UpdatePipeLeak();
            UpdateOilSpawning();
            UpdateConveyor();
            UpdateSwitch();
        }

        private void UpdateSwitch()
        {
            if (m_isSwitching)
            {
                PlaySoundOnce(m_switchesClips[Random.Range(0, m_switchesClips.Count)]);
                m_isSwitching = false;
            }
        }

        private void UpdateConveyor()
        {
            if (m_isConveyorMoving)
            {
                PlaySoundOnce(m_conveyorClips[Random.Range(0, m_conveyorClips.Count)]);
                m_isConveyorMoving = false;
            }
        }

        private void UpdateOilSpawning()
        {
            if (m_isOilSpawning)
            {
                PlaySoundOnce(m_oilClips[Random.Range(0, m_oilClips.Count)]);
                m_isOilSpawning = false;
            }
        }

        private void UpdatePipeLeak()
        {
            if (m_isLeaking && !m_isLeakPlaying)
            {
                m_PipeAudioSource.clip = m_pipeGasClips[Random.Range(0, m_pipeGasClips.Count)];
                m_PipeAudioSource.loop = true;
                m_PipeAudioSource.Play();
                m_isLeakPlaying = true;
            }

            else if (!m_isLeaking)
            {
                m_PipeAudioSource.Stop();
                m_isLeakPlaying = false;
            }
        }

        private void UpdateMusic()
        {
            if (m_isMusicPlaying == false)
            {
                m_isMusicPlaying = true;
                m_AmbientMusicAudioSource.loop = true;
                m_AmbientMusicAudioSource.clip = AmbientMusicClip;
                SetVolume(m_AmbientMusicAudioSource, m_AmbientMusicAudioSource.volume);
                m_AmbientMusicAudioSource.Play();
            }
        }

        private void UpdateGenerator()
        {
            if (!m_isOnFire && !m_isGenPlaying)
            {
                m_generatorAudioSource.clip = m_generatorClip;
                m_generatorAudioSource.loop = true;
                m_generatorAudioSource.Play();
                m_isGenPlaying = true;
            }
            else if(m_isOnFire)
            {
                m_generatorAudioSource.Stop();
                m_isGenPlaying = false;
            }
        }
        public void SetIsOnFire(bool p_isFireState)
        {
            m_isOnFire = p_isFireState;
        }

        public void SetIsPipeLeaking(bool p_pipesState)
        {
            m_isLeaking = p_pipesState;
        }
        
        public void SetIsOilSpawning(bool p_oilSpawn)
        {
            m_isOilSpawning = p_oilSpawn;
        }

        public void SetIsConveyorMoving(bool p_isMoving)
        {
            m_isConveyorMoving = p_isMoving;
        }
        
        public void SetIsSwitching(bool p_isSwitched)
        {
            m_isSwitching = p_isSwitched;
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
            if (m_conveyorClips.Contains(p_audioClip))
                m_OneShotAudioSource.volume = 0.4f;
            else
                m_OneShotAudioSource.volume = 1;
        }

        public void SetVolume (AudioSource p_audioSource, float p_volume)
        {
            p_audioSource.volume = p_volume;
        }
    }
}