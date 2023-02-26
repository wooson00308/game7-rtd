using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Effect : MonoBehaviour
    {
        public void Activate(Action callback = null)
        {
            StartCoroutine(COActivate());

            IEnumerator COActivate()
            {
                gameObject.SetActive(true);

                yield return new WaitForSeconds(1f);
                gameObject.SetActive(false);

                callback?.Invoke();
            }
        }
    }
}