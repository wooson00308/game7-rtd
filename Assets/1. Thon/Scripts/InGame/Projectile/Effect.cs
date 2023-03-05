using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Effect : Unit
    {
        private static Queue<Effect> s_effectPool = new Queue<Effect>();

        public void Activate(Action callback = null)
        {
            StartCoroutine(COActivate(callback));
        }

        private IEnumerator COActivate(Action callback)
        {
            yield return new WaitForSeconds(1f);

            callback?.Invoke();
            Deactivate();
        }

        public static Effect Instantiate(Effect prefab, Vector3 position, Transform parent = null)
        {
            Effect effect;

            if (s_effectPool.Count > 0)
            {
                effect = s_effectPool.Dequeue();
                effect.transform.SetParent(parent);
            }
            else
            {
                effect = Instantiate(prefab, position, Quaternion.identity, parent).GetComponent<Effect>();

                s_effectPool.Enqueue(effect);
            }

            effect.transform.position = position;
            effect.gameObject.SetActive(true);

            return effect;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            s_effectPool.Enqueue(this);
        }
    }
}
