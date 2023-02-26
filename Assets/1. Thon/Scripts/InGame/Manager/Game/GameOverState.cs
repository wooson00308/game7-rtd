using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ����(�й�) �� ���¸� Ȱ��ȭ �Ѵ�.
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