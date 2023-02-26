using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ��� ���̺긦 ���������� �Ϸ��ϸ� �ش� ���¸� Ȱ��ȭ �Ѵ�.
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