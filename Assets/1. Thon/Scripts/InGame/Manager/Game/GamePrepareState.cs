using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ���� �� �غ� �ܰ踦 �����Ѵ�.
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