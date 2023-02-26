using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 타워 생성 및 판매, 업그레이드 등 타워 관련 기능들을 관리한다.
    /// </summary>
    public class TowerManager : MUnit<TowerManager>
    {
        [Header("Tower Manager")]
        [SerializeField] private Transform _towerParent; 
        [SerializeField] private SO_Ship _soShip;

        [SerializeField] private List<Tower> testTowers;

        private Ship _ship;

        public Ship Ship => _ship;

        void GamePrepareEvent()
        {
            _ship = Instantiate(_soShip.PfShip, _towerParent);
        }

        void GameStartEvent()
        {

        }

        public void TryBuildTower()
        {
            _ship.NodePart.TryRandomSpawn(testTowers[Random.Range(0, testTowers.Count)]);
        }

        protected void OnEnable()
        {
            GameManager.PrepareState.AddEvent(GamePrepareEvent);
            GameManager.StartState.AddEvent(GameStartEvent);
        }

        protected void OnDisable()
        {
            GameManager.PrepareState.RemoveEvent(GamePrepareEvent);
            GameManager.StartState.RemoveEvent(GameStartEvent);
        }
    }
}