using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReplayData
{
    public string _stageId;

    [Serializable]
    public class Record
    {
        public int _nodeIndex = -1;
        public int _turretSettingsIndex;
        public int _enemyIndex = -1;
        public float _damageReceived;

        public enum RecordEvent
        {
            TurretBuy,
            TurretUpgrade,
            TurretSell,
            EnemyDamaged,
        }

        public RecordEvent _eventType;
        public float _recordTime;
    }

    [SerializeField] public List<Record> _recordList = new ();

    int _currentIndex;
    public bool IsIndexOver => _currentIndex >= _recordList.Count;

    public void Add(Record record)
    {
        _recordList.Add(record);
    }

    public Record Get()
    {
        return _recordList[_currentIndex++];
    }
}
