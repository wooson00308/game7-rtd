using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    public class DamagePopup : Unit
    {
        TMP_Text _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            Destroy(gameObject, .5f);
        }

        public void SetDamage(int damage, bool isCriticalHit, bool isShield = false)
        {
            if(isShield)
            {
                _text.color = Color.blue;
            }
            
            else if(isCriticalHit)
            {
                _text.color = Color.red;
            }
            
            _text.text = damage.ToString();
        }
    }
}