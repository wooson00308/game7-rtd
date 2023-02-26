using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 게임 종료(패배) 시 상태를 활성화 한다.
    /// </summary>
    public class GameOverState : GameManager.State
    {
        
        public override void Activate()
        {
            base.Activate();

            _activateEvent?.Invoke();
        }
    }
}