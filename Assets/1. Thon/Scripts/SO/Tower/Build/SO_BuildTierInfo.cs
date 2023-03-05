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
        [SerializeField] private int _sellGold;
        [SerializeField] private GameObject _pfGradeColor;
        [SerializeField] private AudioClip _buildTowerClip;
        public int Id => _id;
        public TowerTier Tier => _tier;
        public float BuildWeightRate => _buildWeightRate;
        public int SellGold => _sellGold;
        public int Cost => _cost;
        public GameObject PfGradeColor => _pfGradeColor;
        public AudioClip BuildTowerClip => _buildTowerClip;
    }
}