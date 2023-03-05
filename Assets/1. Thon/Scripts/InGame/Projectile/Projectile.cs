using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Projectile : Unit
    {
        [SerializeField] private SO_Projectile soProjectile;
        public SO_Projectile SoProjectile => soProjectile;

        private Tower attacker;
        private Monster target;

        private Effect attackFx;
        private Vector2 moveVec;

        public void SetAttacker(Tower attacker)
        {
            this.attacker = attacker;
        }

        public void SetTarget(Monster target)
        {
            this.target = target;

            if (this.target == null)
            {
                Deactivate();
            }
        }

        protected void OnEnable()
        {
            Activate();
        }

        protected override void OnFixedUpdate()
        {
            if (GameManager.Instance.IsPause) return;

            base.OnFixedUpdate();

            if (target == null)
            {
                if (moveVec == Vector2.zero)
                {
                    Deactivate();
                }
            }
            else
            {
                moveVec = target.transform.position;
            }

            if (soProjectile.IsRotate)
            {
                var rotVec = moveVec - (Vector2)transform.position;

                float angle = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg + 270; // 270 = 투사체 앞이 Y축 +를 바라보고 있기에
                Quaternion angelAxis = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = angelAxis;
            }

            transform.position = Vector2.MoveTowards(transform.position, moveVec, soProjectile.Speed * Time.fixedDeltaTime);

            if (IsDestination())
            {
                if (attackFx != null)
                {
                    attackFx.Activate(() => Deactivate());
                }
                else
                {
                    Deactivate();
                }

                if (attacker.SOTower.IsAtkSplash)
                {
                    attacker.AttackPart.AttackDamageOrCrt(StageManager.Instance.OnMonsterSplashDamage);
                }
                else
                {
                    if (!target.DeathState.IsDeath)
                    {
                        attacker.AttackPart.AttackDamageOrCrt(target.HealthPart.OnDamaged);
                    }
                }
            }
        }

        private bool IsDestination()
        {
            var distance = Vector2.Distance(transform.position, moveVec);
            return distance <= 0.1f;
        }

        private void Deactivate()
        {
            PoolStorage.ReturnPool(soProjectile.Id, gameObject);
            gameObject.SetActive(false);
        }
    }
}
