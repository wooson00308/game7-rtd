using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ���� (��������)
    /// </summary>
    public class Monster : Unit
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] protected SpriteRenderer _shieldRenderer;

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

        public override void SetUnitId(string id)
        {
            base.SetUnitId(id);

            int order = int.Parse(UnitId);

            _shieldRenderer.sortingOrder = order;

            var mAnimator = Model.GetComponentInChildren<Animator>();
            if (mAnimator != null)
            {
                _spriteRenderer = mAnimator.GetComponentInChildren<SpriteRenderer>();
            }

            if (_spriteRenderer != null) _spriteRenderer.sortingOrder = order;
        }

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