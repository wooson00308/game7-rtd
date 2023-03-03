using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        
        [SerializeField] private bool _isRotate;
        //[SerializeField] private float _rotSpeed;

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

            if (_isRotate)
            {
                var rotVec = _moveVec - (Vector2)transform.position;

                float angle = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg + 270; // 270 = 투사체 앞이 Y축 +를 바라보고 있기에
                Quaternion angelAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = angelAxis;//Quaternion.Slerp(transform.rotation, angelAxis, Time.deltaTime * _rotSpeed);
            }

            transform.position = Vector2.MoveTowards(transform.position, _moveVec, _moveSpeed * Time.fixedDeltaTime);

            if (IsDestination())
            {
                if (_attackFx != null) _attackFx.Activate(() => Destroy(gameObject));

                if (_attacker.SOTower.IsAtkSplash)
                {
                    _attacker.AttackPart.AttackDamageOrCrt(StageManager.Instance.OnMonsterSplashDamage);
                }

                else
                {
                    if (!_target.DeathState.IsDeath)
                    {
                        _attacker.AttackPart.AttackDamageOrCrt(_target.HealthPart.OnDamaged);
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