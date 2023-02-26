using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 인게임에 유저가 보유하고 들고 온 함선
    /// </summary>
    public class Ship : Unit
    {
        [SerializeField] protected SO_Ship _soShip;
        public SO_Ship SOShip => _soShip;

        public abstract class Part : UnitPart
        {
            protected Ship Upper => UpperUnit as Ship;
        }

        public SNodePart NodePart;

        public abstract class State : UnitState
        {
            protected Ship Upper => UpperUnit as Ship;
        }

        protected override void Awake()
        {
            base.Awake();

            SetUnitInfo(_soShip.Id, _soShip.DisplayName);

            AddPart(NodePart);
        }
    }
}