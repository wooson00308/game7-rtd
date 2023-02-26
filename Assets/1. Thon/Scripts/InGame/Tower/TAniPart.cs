using System.Collections;
using UnityEngine;
using Catze.Enum;

namespace Catze
{
    /// <summary>
    /// Ÿ���� �ִϸ��̼�, ����Ʈ ���� ��Ҹ� �����ϴ� ����
    /// </summary>
    public class TAniPart : Tower.Part
    {
        private SO_TAnimation SoAnimation => SOTower.SOTAnimation;
        private Animator _animator;

        protected IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            
            _animator = Upper.Model.GetComponentInChildren<Animator>();
            Upper.SetStateOrNull(Upper.IdleState);
        }

        public void Play(TowerAniState state)
        {
            switch(state)
            {
                case TowerAniState.Idle: 
                    PlayIdleState();
                    break;
                case TowerAniState.Attack:
                    PlayAttackState();
                    break;
            }   
        }

        void PlayIdleState() 
        {
            _animator.SetTrigger(ConstantStrings.ANI_PARAM_TRIGER_TOWER_IDLE);
        }

        void PlayAttackState()
        {
            _animator.SetTrigger(ConstantStrings.ANI_PARAM_TRIGER_TOWER_ATK);
        }

        public void SetAttackSpeed(float attackSpeed)
        {
            _animator.SetFloat(ConstantStrings.ANI_PARAM_FLOAT_TOWER_ATTACK_SPEED, 1/attackSpeed);
        }
    }
}