using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class SoundManager : MUnit<SoundManager>
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _sfxSource;

        [SerializeField] private SO_Sound _soSound;

        public SO_Sound SO_Sound => _soSound;

        protected override void Awake()
        {
            base.Awake();

            _bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume");
            _sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");

            PlayBGM(null);
        }

        public void PlayBGM()
        {
            PlayBGM(null);
        }

        public void PlayBGM(AudioClip clip = null)
        {
            if (_soSound == null)
            {
                LogWarning("SoundObject is null.");
                return;
            }

            if (_soSound.BGM == null && clip == null)
            {
                LogWarning("AudioClip is null.");
                return;
            }

            if (clip != null)
            {
                _bgmSource.clip = clip;
            }
            else
            {
                _bgmSource.clip = _soSound.BGM;
            }

            _bgmSource.Play();
        }

        public void StopBGM()
        {
            _bgmSource.Stop();
        }

        public void PlaySFX()
        {
            PlaySFX(null);
        }

        public void PlaySFX(AudioClip clip = null)
        {
            if (_soSound == null)
            {
                LogWarning("SoundObject is null.");
                return;
            }

            if (_soSound.ButtonClick == null && clip == null)
            {
                LogWarning("AudioClip is null.");
                return;
            }

            _sfxSource.PlayOneShot(_soSound?.ButtonClick ?? clip);
        }

        public void SetBGMVolume(float volume)
        {
            _bgmSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            _sfxSource.volume = volume;
        }

        public float GetBGMVolume()
        {
            return _bgmSource.volume;
        }

        public float GetSFXVolume()
        {
            return _sfxSource.volume;
        }
    }
}