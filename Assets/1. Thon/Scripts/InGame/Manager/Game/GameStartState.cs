using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ���� �� ���¸� Ȱ��ȭ �ϰ�, ���� ������� ���¸� �����Ѵ�.
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