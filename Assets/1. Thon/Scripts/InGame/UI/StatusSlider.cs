using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class StatusSlider : Unit
    {
        [SerializeField] private Transform hpSlider;
        [SerializeField] private Transform shieldSlider;

        public void SetHP(float value)
        {
            if(value < 0)
            {
                value = 0;
            }

            if(value > 1)
            {
                value = 1;
            }

            hpSlider.localScale = new Vector3(value, hpSlider.localScale.y, hpSlider.localScale.z);
        }
        public void SetSheild(float value)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > 1)
            {
                value = 100;
            }

            shieldSlider.localScale = new Vector3(value, shieldSlider.localScale.y, shieldSlider.localScale.z);
        }
    }
}