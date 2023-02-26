using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Catze.API.ReplayData;

namespace Catze.API
{
    public class ReplayState : Replay.State
    {
        Record _currentRecord;

        bool _isEndReplayState;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if(!_isEndReplayState)
            {
                Replay();
            }
        }

        public override void Activate()
        {
            base.Activate();

            _isEndReplayState = false;
            _currentRecord = _replayData.Get();
            StartState();
        }

        public override void Inactivate()
        {
            base.Inactivate();
        }

        public override void StartState()
        {
            base.StartState();
        }

        public override void StopState()
        {
            base.StopState();

            _isEndReplayState = true;
        }

        public void Replay()
        {
            if (_isEndReplayState) return;
            if (_currentRecord._recordTime > Lapse) return;

            Node_Old node = null;

            if (_currentRecord._nodeIndex != -1)
            {
                node = NodeStorage.Instance.GetNode(_currentRecord._nodeIndex);
                UIManager.Instance.NodeSelected(node);
            }

            switch (_currentRecord._eventType)
            {
                case Record.RecordEvent.TurretBuy:
                    TurretShopManager.Instance.NodeSelected(node);
                    var turretCard = TurretShopManager.Instance.Cards[_currentRecord._turretSettingsIndex];
                    turretCard.PlaceTurret();
                    break;
                case Record.RecordEvent.TurretSell:
                    node.SellTurret();
                    break;
                case Record.RecordEvent.TurretUpgrade:
                    node.Turret.TurretUpgrade.UpgradeTurret();
                    break;
                case Record.RecordEvent.EnemyDamaged:
                    var enemy = Spawner.Instance.GetEnemy(_currentRecord._enemyIndex);
                    enemy.EnemyHealth.DealDamage(_currentRecord._damageReceived);
                    break;
            }

            if (_replayData.IsIndexOver)
            {
                _isEndReplayState = true;
                Log($"Replay IndexOver");
                return;
            }

            _currentRecord = _replayData.Get();
        }
    }
}