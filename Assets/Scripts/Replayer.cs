using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReplayData;

/// <summary>
/// Old Scripts
/// </summary>
public class Replayer : MonoBehaviour
{
    public static ReplayData _replayData;
    static Record _currentRecord;

    static bool _isReplayMode;
    public static bool IsReplayMode => _isReplayMode;

    static bool _isReplaying;
    static bool _isRecording;
    static float _recordTime;

    public static float RecordTime => _recordTime;

    static bool _isEndRecordData;

    public static bool CanStartGame { get; set; }

    public void OnTurretSold()
    {
        var record = new Record
        {
            _eventType = Record.RecordEvent.TurretSell,
            _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
            _recordTime = RecordTime
        };

        AddRecord(record);
    }

    public void OnPlaceTurret(TurretSettings settings)
    {
        var record = new Record
        {
            _eventType = Record.RecordEvent.TurretBuy,
            _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
            _recordTime = RecordTime,
            _turretSettingsIndex = settings.turretCardIndex
        };

        AddRecord(record);
    }

    public void OnUpgradeTurret()
    {
        var record = new Record
        {
            _eventType = Record.RecordEvent.TurretUpgrade,
            _nodeIndex = TurretShopManager.Instance.CurrentNodeIndex,
            _recordTime = RecordTime
        };

        AddRecord(record);
    }

    public void OnEnemyHiyReplay(int id, float value)
    {
        var record = new Record
        {
            _eventType = Record.RecordEvent.EnemyDamaged,
            _recordTime = RecordTime,
            _enemyIndex = id,
            _damageReceived = value,
        };

        AddRecord(record);
    }

    void OnEnable()
    {
        PlayFabAPI.Login(callback =>
        {
            if(callback == true)
            {
                ReplayUtil.LoadReplayData((result, data) =>
                {
                    if (result == true)
                    {
                        _replayData = data;
                    }
                    else
                    {
                        _replayData = null;
                    }

                    if (_replayData != null)
                    {
                        _isReplayMode = true;
                        _currentRecord = _replayData.Get();
                    }
                    else
                    {
                        _replayData = new ReplayData();

                        EnemyHealth.OnEnemyHiyRecordEvent += OnEnemyHiyReplay;

                        Node_Old.OnTurretSoldRecordEvent += OnTurretSold;
                        TurretCard.OnPlaceTurretRecordEvent += OnPlaceTurret;
                        TurretUpgrade.OnUpgradeTurretRecordEvent += OnUpgradeTurret;
                    }

                    StartReplayer();

                    CanStartGame = true;
                });
            }
        });
    }

    void OnDisable()
    {
        CanStartGame = false;

        _replayData = null;
        _recordTime = 0;

        _isReplayMode = false;

        _isEndRecordData = false;

        if (_isReplaying == true)
        {
            _isReplaying = false;
        }
        else
        {
            _isRecording = false;

            Node_Old.OnTurretSoldRecordEvent -= OnTurretSold;
            TurretCard.OnPlaceTurretRecordEvent -= OnPlaceTurret;
            TurretUpgrade.OnUpgradeTurretRecordEvent -= OnUpgradeTurret;
        }
    }

    private void FixedUpdate()
    {
        if(_isRecording == true || _isReplaying == true)
        {
            _recordTime += Time.fixedDeltaTime;
        }

        if(_isReplaying == true)
        {
            Replay();
        }
    }

    public static void StartReplayer()
    {
        if(_isReplayMode)
        {
            StartReplay();
        }
        else
        {
            StartRecord();
        }
    }

    public static void StopReplayer()
    {
        if (_isReplayMode)
        {
            StopReplay();
        }
        else
        {
            StopRecord();
        }
    }

    #region Record

    static void StartRecord()
    {
        if (_isRecording == true) return;

        ReplayUtil.Log($"Start Record");

        _recordTime = 0;
        _isRecording = true;
    }

    static void StopRecord()
    {
        if (_isRecording == false) return;

        ReplayUtil.Log($"Stop Record");

        _isRecording = false;
        ReplayUtil.SaveReplayData(_replayData);
        _replayData = null;
    }

    public static void AddRecord(Record record)
    {
        if (_isRecording == false) return;

        ReplayUtil.Log($"Add Record. nodeIndex = {record._nodeIndex}, recordTime = {record._recordTime}, event = {record._eventType}");

        _replayData.Add(record);
    }

    #endregion

    #region Replay

    static void StartReplay()
    {
        if (_isReplaying == true) return;

        ReplayUtil.Log($"Start Replay");

        _recordTime = 0;
        _isReplaying = true;
    }

    static void StopReplay()
    {
        if (_isReplaying == false) return;

        ReplayUtil.Log($"Stop Replay");

        _isReplaying = false;
        _replayData = null;
    }

    public static void Replay()
    {
        if (_isEndRecordData) return;
        if (_currentRecord._recordTime > _recordTime) return;

        ReplayUtil.Log($"Replay : event = {_currentRecord._eventType}, recordTime = {_recordTime}, nodeIndex = {_currentRecord._nodeIndex}");

        Node_Old node = null;

        if(_currentRecord._nodeIndex != -1)
        {
            node = NodeStorage.Instance.GetNode(_currentRecord._nodeIndex);
            UIManager.Instance.NodeSelected(node);
        }

        switch (_currentRecord._eventType)
        {
            case Record.RecordEvent.TurretBuy:
                TurretShopManager.Instance.NodeSelected(node);
                var turretCard = TurretShopManager.Instance.Cards[_currentRecord._turretSettingsIndex];
                turretCard.TryPlaceTurret();
                break;
            case Record.RecordEvent.TurretSell:
                node.TrySellTurret();
                break;
            case Record.RecordEvent.TurretUpgrade:
                node.Turret.TurretUpgrade.TryUpgradeTurret();
                break;
            case Record.RecordEvent.EnemyDamaged:
                var enemy = Spawner.Instance.GetEnemy(_currentRecord._enemyIndex);
                enemy.EnemyHealth.TryDealDamage(_currentRecord._damageReceived);
                break;
        }

        if (_replayData.IsIndexOver)
        {
            _isEndRecordData = true;
            ReplayUtil.Log($"Replay IndexOver");
            return;
        }

        _currentRecord = _replayData.Get();
    }

    #endregion
}
