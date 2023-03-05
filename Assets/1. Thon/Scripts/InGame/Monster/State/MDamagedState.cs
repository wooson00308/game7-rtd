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

            if (Upper.SOMonster.MonsterHitClip != null)
            {
                SoundManager.Instance.PlaySFX(Upper.SOMonster.MonsterHitClip);
            }

            Upper.AniPart.Play(Enum.MonsterAniState.Damaged);

            Upper.SetStateOrNull(Upper.MoveState);
        }
    }
}