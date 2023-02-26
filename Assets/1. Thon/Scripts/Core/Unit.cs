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
        /// 1. ������ Awake�� ������� �ʴ´�. �ʱ�ȭ�� Start���� �����Ѵ�.
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
            * [Lapse�� Activate�� ���� ����]
            * 
            * ���� lapse ����� unitState �ȿ����� ���Ǵµ�, ���� �������� ���ս�ų �ʿ䰡 �ִ°�?
            * state�� �ƴ� �ٸ� ���ֿ��� Lapse�� �۵���ų �� activate�� ����Ǿ�� �ϴµ�.. 
            * 
            * �� �� ����غ��� �����丵 �ؾ� ��
            * 1. State������ ����ϰ� �ű��.
            * 2. Activate �� Inactivate�� Ȱ��ȭ ��Ű�� ������ �����غ���. << ������ �� ���ƺ��̱� ��
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