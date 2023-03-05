using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class SFXPool : Unit
    {
        public AudioSource SFXSource;

        private void OnEnable()
        {
            StartCoroutine(CODuration());
        }

        IEnumerator CODuration()
        {
            yield return new WaitUntil(() => SFXSource.clip != null);
            SFXSource.Play();
            yield return new WaitForSeconds(SFXSource.clip.length);
            gameObject.SetActive(false);
        }
    }
}