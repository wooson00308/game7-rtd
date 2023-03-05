using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    public class DamagePopup : Unit
    {
        private TMP_Text text;

        protected override void Awake()
        {
            base.Awake();
            text = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            StartCoroutine(DeactivateAfterDelay());
        }

        private IEnumerator DeactivateAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            Deactivate();
        }

        public void SetDamage(int damage, bool isCriticalHit, bool isShield = false)
        {
            if (isShield)
            {
                text.color = Color.white;
            }
            else if (isCriticalHit)
            {
                text.color = Color.red;
            }

            text.text = damage.ToString();
        }

        public void Deactivate()
        {
            text.color = Color.white;

            gameObject.SetActive(false);
            PoolStorage.ReturnPool(15589, gameObject);
        }
    }
}
