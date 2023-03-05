using Gpm.Common.ThirdParty.SharpCompress.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class OptionUI : Unit
    {
        bool _isNotMuteBGM = true;
        bool _isNotMuteSFX = true;

        //[SerializeField] private Toggle _bgmToggle;
        //[SerializeField] private Toggle _sfxToggle;

        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        private void OnEnable()
        {
            _bgmSlider.value = SoundManager.Instance.GetBGMVolume();
            _sfxSlider.value = SoundManager.Instance.GetSFXVolume();

            //_bgmToggle.onValueChanged.AddListener(ToggleMuteBGM);
            //_sfxToggle.onValueChanged.AddListener(ToggleMuteSFX);

            _bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        private void OnDisable()
        {
            //_bgmToggle.onValueChanged.RemoveAllListeners();
            //_sfxToggle.onValueChanged.RemoveAllListeners();

            _bgmSlider.onValueChanged.RemoveAllListeners();
            _sfxSlider.onValueChanged.RemoveAllListeners();
        }

        public void SetBGMVolume(float volume)
        {
            if (!_isNotMuteBGM) return;

            SoundManager.Instance.SetBGMVolume(volume);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            if (!_isNotMuteSFX) return;

            SoundManager.Instance.SetSFXVolume(volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        public void ToggleMuteBGM(bool value)
        {
            _isNotMuteBGM = value;

            float volume = value ? _bgmSlider.value : 0;
            SoundManager.Instance.SetBGMVolume(volume);
            PlayerPrefs.SetString("BGMMute", value.ToString());
        }

        public void ToggleMuteSFX(bool value)
        {
            _isNotMuteSFX = value;

            float volume = value ? _sfxSlider.value : 0;
            SoundManager.Instance.SetSFXVolume(volume);
            PlayerPrefs.SetString("SFXMute", value.ToString());
        }
    }
}