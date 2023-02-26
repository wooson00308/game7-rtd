using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Catze.API.ReplayData;

namespace Catze.API
{
    public class RecordState : Replay.State
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void OnTurretSoldRecordEvent()
        {
            var record = new Record
            {
                _eventType = Record.RecordEvent.TurretSell,
                _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
                _recordTime = Lapse
            };

            AddRecord(record);
        }

        public void OnPlaceTurretRecordEvent(TurretSettings settings)
        {
            var record = new Record
            {
                _eventType = Record.RecordEvent.TurretBuy,
                _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
                _recordTime = Lapse,
                _turretSettingsIndex = settings.turretCardIndex
            };

            AddRecord(record);
        }

        public void OnUpgradeTurretRecordEvent()
        {
            var record = new Record
            {
                _eventType = Record.RecordEvent.TurretUpgrade,
                _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
                _recordTime = Lapse
            };

            AddRecord(record);
        }

        public void OnEnemyHiyRecordEvent(int id, float value)
        {
            var record = new Record
            {
                _eventType = Record.RecordEvent.EnemyDamaged,
                _recordTime = Lapse,
                _enemyIndex = id,
                _damageReceived = value,
            };

            AddRecord(record);
        }

        public void GameEndEvent()
        {
            StopState();
        }

        public override void Activate()
        {
            base.Activate();

            LevelManager.GameWinEvent += GameEndEvent;
            LevelManager.GameOverEvent += GameEndEvent;

            _replayData = new ReplayData();

            EnemyHealth.OnEnemyHiyRecordEvent += OnEnemyHiyRecordEvent;

            Node_Old.OnTurretSoldRecordEvent += OnTurretSoldRecordEvent;
            TurretCard.OnPlaceTurretRecordEvent += OnPlaceTurretRecordEvent;
            TurretUpgrade.OnUpgradeTurretRecordEvent += OnUpgradeTurretRecordEvent;

            StartState();
        }

        public override void Inactivate()
        {
            base.Inactivate();

            LevelManager.GameWinEvent -= GameEndEvent;
            LevelManager.GameOverEvent -= GameEndEvent;

            EnemyHealth.OnEnemyHiyRecordEvent -= OnEnemyHiyRecordEvent;

            Node_Old.OnTurretSoldRecordEvent -= OnTurretSoldRecordEvent;
            TurretCard.OnPlaceTurretRecordEvent -= OnPlaceTurretRecordEvent;
            TurretUpgrade.OnUpgradeTurretRecordEvent -= OnUpgradeTurretRecordEvent;
        }

        public override void StartState()
        {
            if (IsUseLapse == true) return;
            base.StartState();
        }

        public override void StopState()
        {
            if (IsUseLapse == false) return;

            base.StopState();

            API.Instance.PlayFab.DataPart.SaveReplayData(_replayData);
            Upper.SetStateOrNull(null); // State 변경됨으로서 Inactivate 발생
        }

        public void AddRecord(Record record)
        {
            if (IsUseLapse == false) return;

            _replayData.Add(record);
        }
    }
}