using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class Node : Unit
    {
        private bool _isEmptyNode = true;
        private Tower _tower;

        [SerializeField] private Transform _towerSpawnPoint;
        [SerializeField] private GameObject _selectEffect;

        public bool IsEmptyNode => _isEmptyNode;
        public Tower Tower => _tower;

        public void SetId(int id)
        {
            _id = id;
        }

        public void SpawnTower(Tower pfTower)
        {
            if (!_isEmptyNode)
            {
                LogError($"{nameof(Node)}, {nameof(SpawnTower)}, is not empty Node!");
                return;
            }

            _isEmptyNode = false;

            _tower = Instantiate(pfTower, _towerSpawnPoint);
        }

        public void RemoveTower()
        {
            if (_isEmptyNode)
            {
                LogError($"{nameof(Node)}, {nameof(RemoveTower)}, is empty Node!");
                return;
            }

            _isEmptyNode = true;

            Destroy(_tower.gameObject);
            _tower = null;
        }
        
        // TODO : Upgrade Tower on Node
    }
}