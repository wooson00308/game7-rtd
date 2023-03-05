using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Catze.PathStorage;

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

        private PathData _currentPath;

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
            Upper.transform.position = _currentPath.transform.position;
        }

        public void Movement()
        {
            if (GameManager.Instance.IsPause) return;
            if (_pathStorage == null) return;

            if (IsDestination())
            {
                _currentPath = _pathStorage.GetPath();
            }

            Upper.Model.rotation = Quaternion.Euler(0, _currentPath.isFlip ? 180 : 0, 0);
            Upper.transform.position = Vector3.MoveTowards(Upper.transform.position, _currentPath.transform.position, _currentSpeed * Time.deltaTime);
        }

        private bool IsDestination()
        {
            var distance = Vector2.Distance(Upper.transform.position, _currentPath.transform.position);
            return distance <= 0.1f;
        }
    }
}