using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 인게임 스테이지를 관리한다
    /// 
    /// 스테이지/웨이브/몬스터
    /// </summary>
    public class StageManager : MUnit<StageManager>
    {
        [Serializable]
        public class Wave
        {
            public string _waveName;
            public float _waveDuration;
            
            public MonsterPath monsterPath;
            public Monster pfMonster;
            public int _maxMonsterSpawnCnt;
            public int _monsterHealth;
            public int _monsterShield;
            public int _dropMoney;

            public float _moveSpeed;
        }

        [Header("Stage Manager")]
        public List<Wave> _waves;
        private Wave _curWave;

        public Transform _monsterParent;

        [SerializeField] private int _maxMonsterCount;
        [SerializeField] private float _maxWarningTime;
        [SerializeField] private float _spawnDelay;

        [Header("UI")]
        [SerializeField] private TMP_Text _waveText;
        [SerializeField] private TMP_Text _waveMonsterCntText;
        [SerializeField] private TMP_Text _waveMaxMonsterCntText;
        [SerializeField] private GameObject _warningObject;

        [Space]

        private int _monsterCount;
        private bool _isCOWarningCount;
        private bool _isWarningState;

        void GamePrepareEvent()
        {
            
        }

        void GameStartEvent()
        {
            // TODO : Wave Start
            StartCoroutine(COStage());
        }

        void GameOverEvent()
        {
            _warningObject.GetComponent<TMP_Text>().text = "Game Over";
            StopAllCoroutines();

            StartCoroutine(CODeleteAllMonster());
        }

        public void OnMonsterDeathEvent()
        {
            _monsterCount--;
        }

        IEnumerator CODeleteAllMonster()
        {
            yield return null;

            var monsters = _monsterParent.GetComponentsInChildren<Monster>();
            foreach(var monster in monsters)
            {
                monster.SetStateOrNull(monster.DeathState);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _waveMaxMonsterCntText.text = $"{_maxMonsterCount} = Gamve Over";

            Activate();
        }

        protected override void FixedUpdate()
        {
            if (GameManager.Instance.IsPause) return;

            base.FixedUpdate();

            _waveMonsterCntText.text = $" {_monsterCount}";

            _isWarningState = _monsterCount >= _maxMonsterCount;
            _warningObject.SetActive(_isWarningState);

            if (_isWarningState)
            {
                StartCoroutine(COWarningCount());
            }
        }

        IEnumerator COWarningCount()
        {
            if (_isCOWarningCount) yield break;
            _isCOWarningCount = true;

            float time = 0;
            while(_isWarningState)
            {
                if (time >= _maxWarningTime)
                {
                    GameManager.Instance.SetStateOrNull(GameManager.OverState);
                }

                time += Time.deltaTime;

                yield return null;
            }

            _isCOWarningCount = false;
        }

        IEnumerator COStage()
        {
            int curWaveIndex = 0;
            while(curWaveIndex < _waves.Count)
            {
                _curWave = _waves[curWaveIndex];

                if (!string.IsNullOrEmpty(_curWave._waveName))
                {
                    _waveText.text = $"{_curWave._waveName}";
                }
                else
                {
                    _waveText.text = $"Wave {curWaveIndex}";
                }

                StartCoroutine(COWave());

                float time = _curWave._waveDuration;

                while(time >= 0)
                {
                    UIManager.Instance.SetTimer(time);
                    time -= Time.deltaTime;

                    yield return null;
                }

                curWaveIndex++;
            }

            GameManager.Instance.SetStateOrNull(GameManager.ClearState);
        }

        IEnumerator COWave()
        {
            int currentSpawnCnt = 0;
            while(currentSpawnCnt < _curWave._maxMonsterSpawnCnt)
            {
                var monster = Instantiate(_curWave.pfMonster, _monsterParent);
                var pathStorage = new PathStorage();
                pathStorage.Setup(_curWave.monsterPath.PathStorage.Paths, false);
                monster.MovePart.SetPathStorage(pathStorage);
                monster.MovePart.SetMoveSpeed(_curWave._moveSpeed);
                monster.HealthPart.SetHealth(_curWave._monsterHealth);
                monster.HealthPart.SetShield(_curWave._monsterShield);
                monster.SetDropMoney(_curWave._dropMoney);

                _monsterCount++;
                monster.DeathState.OnMonsterDeathEvent += OnMonsterDeathEvent;

                currentSpawnCnt++;

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        public void OnMonsterSplashDamage(int damage)
        {
            var monsters = _monsterParent.GetComponentsInChildren<Monster>();
            foreach(var monster in monsters)
            {
                if (monster == null) continue;
                monster.HealthPart.OnDamaged(damage);
            }
        }

        protected override void DelayEnable()
        {
            base.DelayEnable();

            GameManager.PrepareState.AddEvent(GamePrepareEvent);
            GameManager.StartState.AddEvent(GameStartEvent);
            GameManager.OverState.AddEvent(GameOverEvent);
        }

        protected void OnDisable()
        {
            GameManager.PrepareState.RemoveEvent(GamePrepareEvent);
            GameManager.StartState.RemoveEvent(GameStartEvent);
            GameManager.OverState.RemoveEvent(GameOverEvent);
        }
    }
}