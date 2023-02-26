using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 몬스터 유닛 (보스포함)
    /// </summary>
    public class Monster : Unit
    {
        [Header("Monster")]
        [SerializeField] protected Transform _model;
        public Transform Model => _model;

        [SerializeField] protected SO_Monster _soMonster;
        public SO_Monster SOMonster => _soMonster;

        private int _dropMoney;
        public int DropMoney => _dropMoney;

        public abstract class Part : UnitPart
        {
            protected Monster Upper => UpperUnit as Monster;
        }

        [Header("Part")]
        public MAniPart AniPart;
        public MMovePart MovePart;
        public MHealthPart HealthPart;

        public abstract class State : UnitState
        {
            protected Monster Upper => UpperUnit as Monster;
        }

        [Header("State")]
        public MIdleState IdleState;
        public MMoveState MoveState;
        public MDamagedState DamagedState;
        public MDeathState DeathState;

        protected override void Awake()
        {
            base.Awake();

            SetUnitInfo(_soMonster.Id, _soMonster.DisplayName);

            AddPart(AniPart);
            AddPart(MovePart);
            AddPart(HealthPart);

            AddState(IdleState);
            AddState(MoveState);
            AddState(DamagedState);
            AddState(DeathState);

            SetStateOrNull(MoveState);
        }

        public void SetDropMoney(int money)
        {
            _dropMoney = money;
        }
    }
}