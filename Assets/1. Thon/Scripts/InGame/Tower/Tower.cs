using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Tower : Unit
    {
        [SerializeField] protected Transform _model;
        public Transform Model => _model;

        [Header("Tower")]
        [SerializeField] protected SO_Tower _soTower;
        public SO_Tower SOTower => _soTower;

        public abstract class Part : UnitPart
        {
            protected Tower Upper => UpperUnit as Tower;
            protected SO_Tower SOTower => Upper.SOTower;
        }

        [Header("Part")]
        public TAniPart AniPart;
        public TAttackPart AttackPart;

        public abstract class State : UnitState
        {
            protected Tower Upper => UpperUnit as Tower;

            protected TAniPart AniPart => Upper.AniPart;
        }

        [Header("State")]
        public TIdleState IdleState;
        public TAttackState AttackState;

        protected override void Awake()
        {
            base.Awake();

            SetUnitInfo(_soTower.Id, _soTower.DisplayName);

            AddPart(AniPart);
            AddPart(AttackPart);

            AddState(IdleState);
            AddState(AttackState);
        }

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            // 한 프레임 쉬고 상태 변환
            SetStateOrNull(IdleState);
        }
    }
}