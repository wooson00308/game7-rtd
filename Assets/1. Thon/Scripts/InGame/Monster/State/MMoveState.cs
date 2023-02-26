using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MMoveState : Monster.State
    {
        private MMovePart MovePart => Upper.MovePart;
        private MAniPart AniPart => Upper.AniPart;

        public override void Activate()
        {
            base.Activate();

            AniPart.Play(MonsterAniState.Move);
        }

        public override void Inactivate()
        {
            base.Inactivate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            MovePart.Movement();
        }
    }
}
