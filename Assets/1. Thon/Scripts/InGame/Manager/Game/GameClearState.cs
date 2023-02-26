using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 모든 웨이브를 성공적으로 완료하면 해당 상태를 활성화 한다.
    /// </summary>
    public class GameClearState : GameManager.State
    {
        public override void Activate()
        {
            base.Activate();

            _activateEvent?.Invoke();
        }
    }
}