using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MAniPart : Monster.Part
    {
        private Animator _animator;

        protected void Start()
        {
            _animator = Upper.Model.GetComponentInChildren<Animator>();
        }

        public void Play(MonsterAniState state)
        {
            switch (state)
            {
                case MonsterAniState.Idle:
                    //PlayIdleState();
                    break;
                case MonsterAniState.Move:
                    //PlayMoveState();
                    break;
                case MonsterAniState.Damaged:
                    _animator.SetTrigger("Damaged");
                    break;
            }
        }

        void PlayIdleState()
        {
            _animator.SetBool(ConstantStrings.ANI_PARAM_BOOL_MONSTER_IDLE, true);
            _animator.SetBool(ConstantStrings.ANI_PARAM_BOOL_MONSTER_MOVE, false);
        }

        void PlayMoveState()
        {
            _animator.SetBool(ConstantStrings.ANI_PARAM_BOOL_MONSTER_MOVE, false);
            _animator.SetBool(ConstantStrings.ANI_PARAM_BOOL_MONSTER_MOVE, true);
        }
    }
}