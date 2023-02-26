using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ���� ����� �����Ѵ�.
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