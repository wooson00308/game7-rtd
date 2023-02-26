using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Catze
{
    public class Node : Unit
    {
        private SNodePart SNodePart => UpperUnit as SNodePart;

        private bool _isSelected = false;

        [Header("Node")]
        [SerializeField] private bool _isEmptyTower = true;
        [SerializeField] private Tower _tower;
        [SerializeField] private Transform _towerSpawnPoint;

        public UnityEvent SelectEvent;
        public UnityEvent DeselectEvent;

        public bool IsEmptyTower => _isEmptyTower;
        public Tower Tower => _tower;

        public void SetId(int id)
        {
            _id = id;
        }

        public bool SpawnTower(SO_Tower soTower)
        {
            if (!_isEmptyTower)
            {
                Log($"{nameof(Node)}, {nameof(SpawnTower)}, is not empty Node!");
                return false;
            }

            _isEmptyTower = false;

            _tower = Instantiate(soTower.PfTower, _towerSpawnPoint);
            _tower.SetSOTower(soTower);
            _tower.SetNode(this);

            return true;
        }

        public bool AcendTower()
        {
            if (_isEmptyTower)
            {
                LogError($"{nameof(Node)}, {nameof(AcendTower)}, is empty Node!");
                return false;
            }

            var soTower = TowerManager.Instance.GetAcendTower(_tower.SOTower.Tier);

            if(soTower != null)
            {
                Destroy(_tower.gameObject);
                _isEmptyTower = true;

                SpawnTower(soTower);

                return true;
            }

            Debug.Log($"Acend Faild!");

            return false;
        }

        public bool SellTower()
        {
            if (_isEmptyTower)
            {
                LogError($"{nameof(Node)}, {nameof(SellTower)}, is empty Node!");
                return false;
            }

            _isEmptyTower = true;

            _tower.Sell();
            _tower = null;

            return true;
        }

        public void OnTouchNode()
        {
            if (_isEmptyTower)
            {
                SNodePart.DeselectedNode();
                return;
            }

            SNodePart.OnNodeSelect(this);
        }

        [HideInInspector]
        public void SetSelect(bool value = true)
        {
            TryPopupTowerInfo(value);

            if (value)
            {
                SelectEvent?.Invoke();
            }
            else
            {
                DeselectEvent?.Invoke();
            }
        }

        void TryPopupTowerInfo(bool value)
        {
            if (value && !_isEmptyTower)
            {
                UIManager.Instance.ShowTowerInfo(new TowerInfoPopupParam
                {
                    node = this,
                    tower = _tower.SOTower
                });
            }
            else
            {
                UIManager.Instance.HideTowerInfo();
            }
        }

        public void SetNodePart(SNodePart nodePart)
        {
            _upperUnit = nodePart;
        }
    }
}