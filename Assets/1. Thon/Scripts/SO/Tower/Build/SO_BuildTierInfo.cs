using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "SO_BuildTierInfo", menuName = "SO/RTD/Tower/Build/TierInfo", order = 0)]
    public class SO_BuildTierInfo : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private TowerTier _tier;
        [SerializeField] private float _buildWeightRate;
        [SerializeField] private int _cost;
        public int Id => _id;
        public TowerTier Tier => _tier;
        public float BuildWeightRate => _buildWeightRate;
        public int Cost => _cost;
    }
}