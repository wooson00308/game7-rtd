using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TAttackState : Tower.State
    {
        private bool _isRunningCOAttackAniDuration;

        public override void Activate()
        {
            base.Activate();

            StartCoroutine(COAttackAniDuration());
        }
        IEnumerator COAttackAniDuration()
        {
            if (_isRunningCOAttackAniDuration) yield break;
            _isRunningCOAttackAniDuration = true;

            yield return new WaitForSeconds(1/Upper.SOTower.AtkSpeed);

            if(Upper.AttackPart.Target != null)
            {
                AniPart.Play(TowerAniState.Attack);
                Upper.AttackPart.Attack();
            }

            Upper.SetStateOrNull(Upper.IdleState);
            _isRunningCOAttackAniDuration = false;
        }
    }
}