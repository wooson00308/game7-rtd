using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [DisallowMultipleComponent]
    public class Unit : MonoBehaviour
    {
        protected int _id;
        protected string _displayName;

        public int Id => _id;
        public string DisplayName => _displayName;

        protected Unit _upperUnit;
        public Unit UpperUnit => _upperUnit;

        /// <summary>
        /// 1. 파츠는 Awake를 사용하지 않는다. 초기화는 Start에서 진행한다.
        /// </summary>
        public abstract class UnitPart : Unit { }
        public abstract class UnitState : Unit
        {
            float _endTime;

            bool _isUseEndTime;
            bool _isOnEndTime;

            public override void Activate()
            {
                base.Activate();
            }

            public override void Inactivate()
            {
                base.Inactivate();
            }

            protected override void Awake()
            {
                base.Awake();

                _isOnEndTime = false;
            }

            protected override void OnFixedUpdate()
            {
                base.OnFixedUpdate();

                if (_isUseEndTime && _isOnEndTime == false && Lapse >= _endTime)
                {
                    _isOnEndTime = true;
                    OnEndTime();
                }
            }

            protected virtual void OnEndTime()
            {
            }

            public void SetIsUseEndTime(float endTime)
            {
                SetIsUseLapse();
                _isUseEndTime = true;
                _endTime = endTime;
            }
        }

        float _lapse;
        bool _isUseLapse;
        public float Lapse => _lapse;
        public bool IsUseLapse => _isUseLapse;

        protected List<UnitPart> _parts;
        protected List<UnitState> _states;
        public List<UnitPart> Parts => _parts;
        public List<UnitState> States => _states;

        UnitState _curUnitState;
        public UnitState CurUnitState => _curUnitState;

        bool _isActivate;
        bool _isInactivate;

        public bool IsActivate => _isActivate;
        public bool IsInactivate => _isInactivate;

        protected virtual void Awake()
        {
            _isUseLapse = false;
        }

        public virtual void Activate()
        {
            _isActivate = true;
            _isInactivate = false;
        }

        public virtual void Inactivate()
        {
            _isActivate = false;
            _isInactivate = true;
        }

        protected virtual void FixedUpdate()
        {
            /*
            * [Lapse와 Activate에 대한 고찰]
            * 
            * 현재 lapse 기능은 unitState 안에서만 사용되는데, 굳이 유닛으로 통합시킬 필요가 있는가?
            * state가 아닌 다른 유닛에서 Lapse를 작동시킬 때 activate도 선행되어야 하는데.. 
            * 
            * 좀 더 고민해보고 리펙토링 해야 함
            * 1. State에서만 사용하게 옮긴다.
            * 2. Activate 및 Inactivate를 활성화 시키는 방향을 구색해본다. << 이쪽이 더 좋아보이긴 함
            */

            if (!_isActivate) return;

            if (_isUseLapse)
            {
                _lapse += Time.deltaTime;
            }

            OnFixedUpdate();
        }

        protected virtual void SetIsUseLapse(bool value = true)
        {
            if (value)
            {
                _lapse = 0;
            }

            _isUseLapse = value;
        }

        protected virtual void OnFixedUpdate()
        {

        }

        public void AddPart(UnitPart part)
        {
            if (_parts == null)
            {
                _parts = new();
            }

            _parts.Add(part);
            part.SetUpperUnit(this);
        }

        public void AddState(UnitState state)
        {
            if (_states == null)
            {
                _states = new();
            }

            _states.Add(state);
            state.SetUpperUnit(this);
        }

        public void SetStateOrNull(UnitState stateOrNull)
        {
            Log($"{nameof(SetStateOrNull)} {(_curUnitState == null ? "null" : _curUnitState.LogName)} -> {(stateOrNull == null ? "null" : stateOrNull.LogName)}");

            _curUnitState?.Inactivate();
            _curUnitState = stateOrNull;
            OnSetState(stateOrNull);
            _curUnitState?.Activate();
        }

        protected virtual void OnSetState(UnitState state)
        {
        }

        void SetUpperUnit(Unit unit)
        {
            _upperUnit = unit;
        }

        protected virtual void DisableUnit() { }

        protected void SetUnitInfo(int id, string displayName)
        {
            _id = id;
            _displayName = displayName;
        }

        #region Logger 

        [Header("Log")]
        [SerializeField]
        protected bool onLogMode;
        [SerializeField]
        protected bool onLogWarningMode;
        [SerializeField]
        protected bool onLogErrorMode;

        public virtual string LogName => $"{name} ({GetType().Name})";

        public void Log(object obj)
        {
            if (onLogMode)
            {
                Debug.Log(obj.ToString());
            }
        }

        public void LogWarning(object obj)
        {
            if (onLogWarningMode)
            {
                Debug.LogWarning(obj.ToString());
            }
        }

        public void LogError(object obj)
        {
            if (onLogErrorMode)
            {
                Debug.LogError(obj.ToString());
            }
        }

        #endregion
    }
}