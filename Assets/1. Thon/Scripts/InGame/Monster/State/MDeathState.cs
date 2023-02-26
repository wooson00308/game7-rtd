using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MDeathState : Monster.State
    {
        private bool _isDeath;
        public bool IsDeath => _isDeath;
        public Action OnMonsterDeathEvent;

        public override void Activate()
        {
            if (_isDeath) return;
            base.Activate();

            TowerManager.Instance.AddMoney(Upper.DropMoney);

            OnMonsterDeathEvent?.Invoke();
            OnMonsterDeathEvent = () => { };

            if (Upper != null)
            {
                Destroy(Upper.gameObject);
            }
        }
    }
}
