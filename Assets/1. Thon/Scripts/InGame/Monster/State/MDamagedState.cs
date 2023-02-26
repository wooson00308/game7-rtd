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
            yield return new WaitForSeconds(0);

            Upper.SetStateOrNull(Upper.MoveState);
        }
    }
}