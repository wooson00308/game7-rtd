using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class AniClip : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> clips;
        public void PlayClip(int index)
        {
            SoundManager.Instance.PlaySFX(clips[index]);
        }
    }
}