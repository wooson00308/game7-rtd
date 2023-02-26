using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MMovePart : Monster.Part
    {
        [Header("Monster Move Part")]
        [SerializeField] private PathStorage _pathStorage;
        public PathStorage PathStorage => _pathStorage;
        public int CurIndex
        {
            get
            {
                if(_pathStorage == null)
                {
                    return 0;
                }

                return _pathStorage.CurIndex;
            }
        }

        private Vector2 _currentPath;

        private float _defaultSpeed;
        private float _currentSpeed;

        public void SetMoveSpeed(float value)
        {
            _defaultSpeed = value;
            ChangeMoveSpeed(_defaultSpeed);
        }

        public void ResetMoveSpeed()
        {
            _currentSpeed = _defaultSpeed;
        }

        public void ChangeMoveSpeed(float value)
        {
            _currentSpeed = value;
        }

        public void SetPathStorage(PathStorage mPath)
        {
            _pathStorage = mPath;
            _currentPath = _pathStorage.GetPath();
            Upper.transform.position = _currentPath;
        }

        public void Movement()
        {
            if (GameManager.Instance.IsPause) return;
            if (_pathStorage == null) return;

            if (IsDestination())
            {
                _currentPath = _pathStorage.GetPath();
            }

            Upper.transform.position = Vector3.MoveTowards(Upper.transform.position, _currentPath, _currentSpeed * Time.deltaTime);
        }

        private bool IsDestination()
        {
            var distance = Vector2.Distance(Upper.transform.position, _currentPath);
            return distance <= 0.1f;
        }
    }
}