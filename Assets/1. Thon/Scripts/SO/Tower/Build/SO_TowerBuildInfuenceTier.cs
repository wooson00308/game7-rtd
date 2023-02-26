using Catze.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "SO_TowerBuildInfuenceTier", menuName = "SO/RTD/Tower/Build/Influence/Tier", order = 0)]
    public class SO_TowerBuildInfuenceTier : ScriptableObject
    {
        [SerializeField] private int _influence;
        [SerializeField] private SO_BuildTierInfo _soTierInfo;
        [SerializeField] private SO_Tower _soTower;

        public int Influence => _influence;
        public SO_BuildTierInfo SOTierInfo => _soTierInfo;
        public SO_Tower SOTower => _soTower;
    }
}