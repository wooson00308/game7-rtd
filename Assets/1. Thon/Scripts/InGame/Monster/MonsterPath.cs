using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Catze.PathStorage;

namespace Catze
{
    public class MonsterPath : Unit
    {
        [Header("Monster Path")]
        [SerializeField] private bool _isUseCustomPath;
        [SerializeField] private bool _isLoop;
        [SerializeField] private bool _isLocalPath;
        [SerializeField] private List<PathData> customPaths;
        [SerializeField] private PathStorage _pathStorage;
        public PathStorage PathStorage => _pathStorage;

        protected override void Awake()
        {
            base.Awake();

            var paths = new List<PathData>();

            if (_isUseCustomPath)
            {
                foreach(var path in customPaths)
                {
                    paths.Add(path);
                }

                _pathStorage.Setup(paths, _isLocalPath, _isLoop);
            }
        }
    }
}