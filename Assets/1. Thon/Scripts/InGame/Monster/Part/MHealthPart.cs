using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MHealthPart : Monster.Part
    {
        private int _currentHealth;
        private int _currentShield;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetHealth(int health)
        {
            _currentHealth = health;
        }

        public void SetShield(int shield)
        {
            _currentShield = shield;
        }

        public void OnDamaged(int damage)
        {
            damage -= _currentShield;

            if(damage <= 0)
            {
                return;
            }

            _currentHealth -= damage;

            if(_currentHealth <= 0)
            {
                Upper.SetStateOrNull(Upper.DeathState);
            }
            else
            {
                Upper.SetStateOrNull(Upper.DamagedState);
            }
        }
    }
}