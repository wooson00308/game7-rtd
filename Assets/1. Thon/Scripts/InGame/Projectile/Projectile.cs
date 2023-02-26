using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Projectile : Unit
    {
        private Tower _attacker;
        private Monster _target;

        [Header("Projectile")]
        private Effect _attackFx;
        [SerializeField] private float _moveSpeed;

        private Vector2 _moveVec;

        public void SetAttacker(Tower attacker)
        {
            _attacker = attacker;
        }

        public void SetTarget(Monster monster)
        {
            _target = monster;
            if(_target == null)
            {
                Destroy(gameObject);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Activate();
        }

        protected override void OnFixedUpdate()
        {
            if (GameManager.Instance.IsPause) return;

            base.OnFixedUpdate();

            if (_target == null)
            {
                if (_moveVec == Vector2.zero) Destroy(gameObject);
            }
            else
            {
                _moveVec = _target.transform.position;
            }

            transform.position = Vector2.MoveTowards(transform.position, _moveVec, _moveSpeed * Time.fixedDeltaTime);


            if (IsDestination())
            {
                if (_attackFx != null) _attackFx.Activate(() => Destroy(gameObject));

                if (_attacker.SOTower.IsAtkSplash)
                {
                    StageManager.Instance.OnMonsterSplashDamage(_attacker.AttackPart.AttackDamage);
                }

                else
                {
                    if (!_target.DeathState.IsDeath)
                    {
                        _target.HealthPart.OnDamaged(_attacker.AttackPart.AttackDamage);
                    }
                }

                Destroy(gameObject);
            }
        }

        private bool IsDestination()
        {
            var distance = Vector2.Distance(transform.position, _moveVec);
            return distance <= 0.1f;
        }
    }
}