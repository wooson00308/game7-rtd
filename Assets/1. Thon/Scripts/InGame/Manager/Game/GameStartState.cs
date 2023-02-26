using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// ���� ���� �� ���¸� Ȱ��ȭ �ϰ�, ���� ������� ���¸� �����Ѵ�.
    /// </summary>
    public class GameStartState : GameManager.State
    {
        [SerializeField] private TMP_Text _timerText;

        public override void Activate()
        {
            base.Activate();

            _activateEvent?.Invoke();
        }
    }
}