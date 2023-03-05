using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MDamagedState : Monster.State
    {
        public override void Activate()
        {
            base.Activate();

            StartCoroutine(COActivate());
        }

        IEnumerator COActivate()
        {
            yield return null;

            Upper.AniPart.Play(Enum.MonsterAniState.Damaged);

            Upper.SetStateOrNull(Upper.MoveState);
        }
    }
}