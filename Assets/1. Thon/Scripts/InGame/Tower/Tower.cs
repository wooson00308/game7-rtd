using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 타워의 Upper는 Node
    /// </summary>
    public class Tower : Unit
    {
        [SerializeField] protected Transform _model;
        public Transform Model => _model;

        protected SO_Tower _soTower;
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

        public void SetSOTower(SO_Tower soTower)
        {
            _soTower = soTower;
        }

        protected override void Awake()
        {
            base.Awake();

            AddPart(AniPart);
            AddPart(AttackPart);

            AddState(IdleState);
            AddState(AttackState);
        }

        IEnumerator Start()
        {
            Instantiate(_soTower.PfGradeColor, _model);
            Instantiate(_soTower.PfTowerModel, _model);

            yield return new WaitForEndOfFrame();

            SetUnitInfo(_soTower.Id, _soTower.DisplayName);
            transform.name = _soTower.DisplayName;
        }

        public void SetNode(Node node)
        {
            _upperUnit = node;
        }

        public void Sell()
        {
            TowerManager.Instance.AddMoney(_soTower.SellCost);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}