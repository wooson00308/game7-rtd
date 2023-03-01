using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Catze
{
    public class MHealthPart : Monster.Part
    {
        private int _currentHealth;
        private int _maxShield;
        private int _currentShield;

        private bool _isRunCODelayRecoveryShield = false;
        private bool _isRunCORecoveryShield = false;

        [SerializeField] float _delayRecoveryShieldTime;

        private float _currentDelayRecoveryShieldTime;

        protected override void Awake()
        {
            base.Awake();

            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(!_isRunCODelayRecoveryShield)
            {
                if (_currentShield < _maxShield)
                {
                    StartCoroutine(CODelayRecoveryShield());
                }
            }
        }

        IEnumerator CODelayRecoveryShield()
        {
            if (_isRunCORecoveryShield) yield break;
            if (_isRunCODelayRecoveryShield) yield break;
            _isRunCODelayRecoveryShield = true;

            _currentDelayRecoveryShieldTime = 0;

            while (_currentDelayRecoveryShieldTime >= _delayRecoveryShieldTime)
            {
                _currentDelayRecoveryShieldTime += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(CORecoveryShield());

            _isRunCODelayRecoveryShield = false;
        }

        IEnumerator CORecoveryShield()
        {
            if (_isRunCORecoveryShield) yield break;
            _isRunCORecoveryShield = true;

            while (!_isRunCODelayRecoveryShield || _currentShield < _maxShield)
            {
                _currentShield += (int)Time.deltaTime;
                yield return null;
            }

            _isRunCORecoveryShield = false;
        }

        public void SetHealth(int health)
        {
            _currentHealth = health;
        }

        public void SetShield(int shield)
        {
            _maxShield = shield;
            _currentShield = shield;
        }

        public void OnDamaged(int damage)
        {
            _currentDelayRecoveryShieldTime = 0;

            if (_currentShield > 0)
            {
                damage -= _currentShield;
                
                _currentShield -= damage;
                if(_currentShield < 0)
                {
                    _currentShield = 0;
                }
            }
            
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