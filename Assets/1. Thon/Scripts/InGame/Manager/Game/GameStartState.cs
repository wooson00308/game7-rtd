using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 게임 시작 시 상태를 활성화 하고, 게임 종료까지 상태를 유지한다.
    /// </summary>
    public class GameStartState : GameManager.State
    {
        public override void Activate()
        {
            base.Activate();

            _activateEvent?.Invoke();
        }
    }
}