using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Catze
{
    public class SoundManager : MUnit<SoundManager>
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioSource _sfxSource;
        [SerializeField] private SFXPool _sfxObjectPrefab;
        [SerializeField] private int _sfxObjectPoolSize = 10;

        private List<SFXPool> _sfxPoolList;

        [SerializeField] private SO_Sound _soSound;

        public SO_Sound SO_Sound => _soSound;

        protected override void Awake()
        {
            base.Awake();

            _bgmSource.volume = PlayerPrefs.GetFloat("BGMVolume");
            _sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");

            PlayBGM(null);

            // SFX ������Ʈ Ǯ �ʱ�ȭ
            _sfxPoolList = new List<SFXPool>();
            for (int i = 0; i < _sfxObjectPoolSize; i++)
            {
                SFXPool sfxPool = Instantiate(_sfxObjectPrefab, _sfxSource.transform);
                sfxPool.gameObject.SetActive(false);
                _sfxPoolList.Add(sfxPool);
            }
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

        public void PlayButtonSFX()
        {
            PlaySFX(SO_Sound.ButtonClick);
        }

        public void PlaySFX(AudioClip clip = null)
        {
            if (clip == null)
            {
                LogWarning("AudioClip is null.");
                return;
            }

            // SFX ������Ʈ Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�Ƽ� ��Ȱ��ȭ
            SFXPool sfxPool = _sfxPoolList.Find(pool => pool.gameObject.activeSelf == false);
            if (sfxPool == null)
            {
                // SFX ������Ʈ Ǯ ũ�⸦ �ʰ��ϸ� ���ο� ������Ʈ�� ����
                sfxPool = Instantiate(_sfxObjectPrefab, _sfxSource.transform);
                _sfxPoolList.Add(sfxPool);
            }

            // SFX AudioSource�� ���� ȿ���� ���
            SFXPool sfxAudioSource = sfxPool.GetComponent<SFXPool>();
            sfxAudioSource.SFXSource.volume = _sfxSource.volume;
            sfxAudioSource.SFXSource.clip = clip;
            sfxPool.gameObject.SetActive(true);
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
