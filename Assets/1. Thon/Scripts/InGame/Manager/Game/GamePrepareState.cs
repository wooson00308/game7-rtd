using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 게임 시작 전 준비 단계를 관리한다.
    /// </summary>
    public class GamePrepareState : GameManager.State
    {
        public override void Activate()
        {
            base.Activate();

            _activateEvent?.Invoke();

            Upper.SetStateOrNull(Upper._startState);
        }
    }
}