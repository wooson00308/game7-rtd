using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 타워의 공격을 담당하는 파츠.
    /// 
    /// 범위 안의 적을 타게팅
    /// </summary>
    public class TAttackPart : Tower.Part
    {
        private float _defaultAttackSpeed;
        private float _currentAttackSpeed;

        private int _defaultAttackDamage;
        private int _currentAttackDamage;

        private float _defaultAttackCrtRate;
        private float _currentAttackCrtRate;

        private int _defaultAttackCrtDamage;
        private int _currentAttackCrtDamage;
        
        private Projectile _projectile;

        public float AttackSpeed => _currentAttackSpeed;
        public int AttackDamage
        {
            get
            {
                if (Util.GetRateResult(_currentAttackCrtRate))
                {
                    Log($"Critical! {_currentAttackDamage} -> {_currentAttackCrtDamage}");
                    return _currentAttackCrtDamage;
                }

                int level = TowerManager.Instance.GetTowerUpgradeLevel(SOTower.InfluenceInt);
                return _currentAttackDamage * level;
            }
        }
        public float AttackCrtRate => _currentAttackCrtRate;
        public int AttackCrtDamage => _currentAttackCrtDamage;

        public abstract class Part : UnitPart
        {
            protected TAttackPart Upper => UpperUnit as TAttackPart;
        }

        private TTargetSelector _targetSelector;

        public Monster Target => _targetSelector.Target;

        protected override void Awake()
        {
            base.Awake();
            
            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (Target == null) return;
            if (Upper.CurUnitState == Upper.AttackState) return;

            Upper.SetStateOrNull(Upper.AttackState);
        }

        public void Attack()
        {
            var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
            projectile.SetAttacker(Upper);
            projectile.SetTarget(Target);
        }

        protected IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            
            _targetSelector = Upper.Model.GetComponentInChildren<TTargetSelector>();
            _targetSelector.SetRange(SOTower.AtkRange);
            _projectile = SOTower.PfProjectile;
            
            SetAttackDamage(SOTower.AtkDamage);
            SetAttackSpeed(SOTower.AtkSpeed);
            SetAttackCrtRate(SOTower.AtkCrtRate);
            SetAttackCrtDamage(SOTower.AtkCrtDamage);

            AddPart(_targetSelector);
        }

        public void ResetAttackSpeed()  
        {
            SetAttackSpeed(_defaultAttackSpeed);
        }

        public void ResetAttackDamage() 
        {
            SetAttackDamage(_defaultAttackDamage);
        }

        public void ResetAttackCrtRate()
        {
            SetAttackCrtRate(_defaultAttackCrtRate);
        }

        public void ResetAttackCrtDamage()
        {
            SetAttackCrtDamage(_defaultAttackCrtDamage);
        }

        public void SetAttackSpeed(float attackSpeed, bool isDefault = false)
        {
            if (isDefault)
            {
                _defaultAttackSpeed = attackSpeed;
            }

            _currentAttackSpeed = attackSpeed;
        }

        public void SetAttackDamage(int attackDamage, bool isDefault = false)
        {
            if (isDefault)
            {
                _defaultAttackDamage = attackDamage;
            }

            _currentAttackDamage = attackDamage;
        }

        public void SetAttackCrtRate(float attackCrtRate, bool isDefault = false)
        {
            if (isDefault)
            {
                _defaultAttackCrtRate = attackCrtRate;
            }

            _currentAttackCrtRate = attackCrtRate;
        }

        public void SetAttackCrtDamage(int attackCrtDamage, bool isDefault = false)
        {
            if(isDefault)
            {
                _defaultAttackCrtDamage = attackCrtDamage;
            }

            _currentAttackCrtDamage = attackCrtDamage;
        } 
    }
}