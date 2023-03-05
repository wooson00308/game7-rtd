using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Catze
{
    public class MHealthPart : Monster.Part
    {
        private float _maxHealth;
        private float _currentHealth;
        private float _maxShield;
        private float _currentShield;

        private bool _isRunCODelayRecoveryShield = false;
        private bool _canRecoveryShield = false;
        private bool _isDeath = false;

        float _delayRecoveryShieldTime;
        float _maxShieldAlpha;

        private float _currentDelayRecoveryShieldTime;
        [SerializeField] SpriteRenderer _shieldObject;
        [SerializeField] StatusSlider _statusSlider;
        
        protected override void Awake()
        {
            base.Awake();

            _delayRecoveryShieldTime = Upper.SOMonster.DelayRecoveryTime;
            _maxShieldAlpha = _shieldObject.color.a;

            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            _shieldObject.gameObject.SetActive(_currentShield > 0);
            _statusSlider.SetHP(_currentHealth / _maxHealth * 1f);

            bool isFullCondition = _currentHealth == _maxHealth && _currentShield == _maxShield;
            _statusSlider.gameObject.SetActive(!isFullCondition);

            if (_maxShield > 0)
            {
                _statusSlider.SetSheild(_currentShield / _maxShield * 1f);

                _shieldObject.color = new Color(_shieldObject.color.r, _shieldObject.color.g, _shieldObject.color.b, _currentShield / _maxShield * _maxShieldAlpha);

                if (_canRecoveryShield)
                {
                    if (_currentShield < _maxShield)
                    {
                        _currentShield += Upper.SOMonster.RecoverySpeed * Time.fixedDeltaTime;
                    }
                    else
                    {
                        _currentShield = _maxShield;
                    }
                }
                else
                {
                    if (!_isRunCODelayRecoveryShield)
                    {
                        if (_currentShield < _maxShield)
                        {
                            StartCoroutine(CODelayRecoveryShield());
                        }
                    }
                }
            }
        }

        IEnumerator CODelayRecoveryShield()
        {
            if (_isRunCODelayRecoveryShield) yield break;
            _isRunCODelayRecoveryShield = true;

            _currentDelayRecoveryShieldTime = 0;

            _canRecoveryShield = false;

            while (_currentDelayRecoveryShieldTime < _delayRecoveryShieldTime)
            {
                _currentDelayRecoveryShieldTime += Time.deltaTime;
                yield return null;
            }

            _canRecoveryShield = true;

            _isRunCODelayRecoveryShield = false;
        }

        public void SetHealth(int health)
        {
            _maxHealth = health;
            _currentHealth = health;
        }

        public void SetShield(int shield)
        {
            _maxShield = shield;
            _currentShield = shield;
        }

        public void OnDamaged(int damage, bool isCritical)
        {
            _canRecoveryShield = false;
            _currentDelayRecoveryShieldTime = 0;

            if (_isDeath)
            {
                return;
            }

            if (damage <= 0)
            {
                return;
            }

            if (Upper.SOMonster.MonsterHitClip != null)
            {
                SoundManager.Instance.PlaySFX(Upper.SOMonster.MonsterHitClip);
            }
            
            int healthDamage = damage;

            if (_currentShield > 0)
            {
                var shieldDamagePopup = PoolStorage.Pooling(15589, Upper.SOMonster.PfDamagePopup.GetComponent<DamagePopup>(), transform.position);
                shieldDamagePopup.SetDamage(damage, isCritical, true);

                var tempShield = (int)_currentShield;
                _currentShield -= damage;

                if (_currentShield <= 0)
                {
                    _currentShield = 0;
                    healthDamage -= tempShield;

                    if (healthDamage <= 0) return;
                } 
                
                else return;
            }

            var damagePopup = PoolStorage.Pooling(15589, Upper.SOMonster.PfDamagePopup.GetComponent<DamagePopup>(), transform.position);
            damagePopup.SetDamage(healthDamage, isCritical);

            _currentHealth -= healthDamage;

            if(_currentHealth <= 0)
            {
                _isDeath = true;
                Upper.SetStateOrNull(Upper.DeathState);
            }
            else
            {
                Upper.SetStateOrNull(Upper.DamagedState);
            }
        }
    }
}