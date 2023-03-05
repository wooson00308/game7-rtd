using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "SO/RTD/Sound")]
    public class SO_Sound : ScriptableObject
    {
        [SerializeField] private AudioClip _bgm;
        [SerializeField] private AudioClip _buttonClick;

        public AudioClip BGM => _bgm;
        public AudioClip ButtonClick => _buttonClick;
    }
}