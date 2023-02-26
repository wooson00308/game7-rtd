using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// Ÿ���� ������ ����ϴ� ����.
    /// 
    /// ���� ���� ���� Ÿ����
    /// </summary>
    public class TAttackPart : Tower.Part
    {
        private float _defaultAttackSpeed;
        private float _currentAttackSpeed;

        private int _defaultAttackDamage;
        private int _currentAttackDamage;

        public float AttackSpeed => _currentAttackSpeed;
        public int AttackDamage => _currentAttackDamage;

        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            _defaultAttackSpeed = SOTower.AtkSpeed;
            _currentAttackSpeed = _defaultAttackSpeed;

            _defaultAttackDamage = SOTower.AtkDamage;
            _currentAttackDamage = _defaultAttackDamage;
        }

        public void ResetAttackSpeed()  
        {
            SetAttackSpeed(_defaultAttackSpeed);
        }

        public void ResetAttackDamage() 
        {
            SetAttackDamage(_defaultAttackDamage);
        }

        public void SetAttackSpeed(float attackSpeed)
        {
            _currentAttackSpeed = attackSpeed;
            Upper.AniPart.SetAttackSpeed(_currentAttackSpeed);
        }

        public void SetAttackDamage(int attackDamage)
        {
            _currentAttackDamage = attackDamage;
        }
    }
}