using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catze
{
    public class Projectile : Unit
    {
        [SerializeField] private SO_Projectile _soProjectile;
        public SO_Projectile SoProjectile => _soProjectile;
        
        private Tower _attacker;
        private Monster _target;
        
        private Effect _attackFx;
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

            if (SoProjectile.IsRotate)
            {
                var rotVec = _moveVec - (Vector2)transform.position;

                float angle = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg + 270; // 270 = 투사체 앞이 Y축 +를 바라보고 있기에
                Quaternion angelAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = angelAxis;
            }

            transform.position = Vector2.MoveTowards(transform.position, _moveVec, SoProjectile.Speed * Time.fixedDeltaTime);

            // 목적지에 도착하면
            if (IsDestination())
            {
                // 이팩트 발생
                if (_attackFx != null) _attackFx.Activate(() => Destroy(gameObject));

                // 데미지 적용
                
                // 스플레쉬
                if (_attacker.SOTower.IsAtkSplash)
                {
                    _attacker.AttackPart.AttackDamageOrCrt(StageManager.Instance.OnMonsterSplashDamage);
                }

                // 단일 공격
                else
                {
                    if (!_target.DeathState.IsDeath)
                    {
                        _attacker.AttackPart.AttackDamageOrCrt(_target.HealthPart.OnDamaged);
                    }
                }

                // 투사체 파괴
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// 목적지에 도착했는지 확인
        /// </summary>
        /// <returns></returns>
        private bool IsDestination()
        {
            var distance = Vector2.Distance(transform.position, _moveVec);
            return distance <= 0.1f;
        }
    }
}