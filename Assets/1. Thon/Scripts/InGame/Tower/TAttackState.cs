using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TAttackState : Tower.State
    {
        public override void Activate()
        {
            base.Activate();

            AniPart.Play(TowerAniState.Attack);
        }
    }
}