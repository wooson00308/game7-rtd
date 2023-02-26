using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 몬스터 관련 기능을 관리한다.
    /// </summary>
    public class WaveManager : MUnit<WaveManager>
    {
        void GamePrepareEvent()
        {
            
        }

        void GameStartEvent()
        {
            // TODO : Wave Start
        }

        protected void OnEnable()
        {
            GameManager.PrepareState.AddEvent(GamePrepareEvent);
            GameManager.StartState.AddEvent(GameStartEvent);
        }

        protected void OnDisable()
        {
            GameManager.PrepareState.RemoveEvent(GamePrepareEvent);
            GameManager.StartState.RemoveEvent(GameStartEvent);
        }
    }
}