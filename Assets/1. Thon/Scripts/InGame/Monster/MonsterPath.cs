using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MonsterPath : Unit
    {
        [Header("Monster Path")]
        [SerializeField] private bool _isUseCustomPath;
        [SerializeField] private bool _isLoop;
        [SerializeField] private bool _isLocalPath;
        [SerializeField] private List<Transform> customPaths;
        [SerializeField] private PathStorage _pathStorage;
        public PathStorage PathStorage => _pathStorage;

        protected override void Awake()
        {
            base.Awake();

            var paths = new List<Vector2>();

            if (_isUseCustomPath)
            {
                foreach(var path in customPaths)
                {
                    paths.Add(path.position);
                }

                _pathStorage.Setup(paths, _isLocalPath, _isLoop);
            }
        }
    }
}