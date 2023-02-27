using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Catze
{
    public class GameManager : MUnit<GameManager>
    {
        public bool IsPause => _isPause;

        public Action PauseAction;
        public Action ResumeAction;

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
            _isPause = false;
        }

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            AddState(_prepareState);
            AddState(_startState);
            AddState(_overState);
            AddState(_clearState);

            // 한 프레임 쉬고 상태 변환
            SetStateOrNull(_prepareState);
        }

        public void Pause()
        {
            _isPause = !_isPause;

            if(_isPause)
            {
                Time.timeScale = 0;
                PauseAction?.Invoke();
            }
            else
            {
                Time.timeScale = 1;
                ResumeAction?.Invoke();
            }
        }

        public void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}