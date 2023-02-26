using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class GameManager : MUnit<GameManager>
    {
        public abstract class State : UnitState
        {
            protected GameManager Upper => UpperUnit as GameManager;

            protected Action _activateEvent;

            public void AddEvent(Action action)
            {
                _activateEvent += action;
            }

            public void RemoveEvent(Action action)
            {
                _activateEvent -= action;
            }
        }

        [Header("State")]
        public GamePrepareState _prepareState;
        public GameStartState _startState;
        public GameOverState _overState;
        public GameClearState _clearState;

        public static GamePrepareState PrepareState => Instance._prepareState;
        public static GameStartState StartState => Instance._startState;
        public static GameOverState OverState => Instance._overState;
        public static GameClearState ClearState => Instance._clearState;

        protected override void Awake()
        {
            base.Awake();

            AddState(_prepareState);
            AddState(_startState);
            AddState(_overState);
            AddState(_clearState);
        }

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            // 한 프레임 쉬고 상태 변환
            SetStateOrNull(_prepareState);
        }
    }
}