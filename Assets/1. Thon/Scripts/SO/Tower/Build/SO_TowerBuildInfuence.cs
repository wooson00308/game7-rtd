using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "SO_TowerBuildInfuence", menuName = "SO/RTD/Tower/Build/Infuence", order = 0)]
    public class SO_TowerBuildInfuence : ScriptableObject
    {
        [SerializeField] private int _influence;
        [SerializeField] private List<SO_TowerBuildInfuenceTier> _soTowerBuildInfluenceTiers;

        public int Influence => _influence;
        public List<SO_TowerBuildInfuenceTier> SOTowerBuildInfuenceTiers => _soTowerBuildInfluenceTiers;
    }
}