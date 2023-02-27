using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MUnit<T> : Unit where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject newInstance = new GameObject();
                        instance = newInstance.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void OnEnable()
        {
            instance = this as T;

            StartCoroutine(COEnable());
        }

        private IEnumerator COEnable()
        {
            yield return new WaitForEndOfFrame();

            DelayEnable();
        }

        protected virtual void DelayEnable()
        {

        }
    }
}