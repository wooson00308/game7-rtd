using System.Collections.Generic;
using UnityEngine;
using Catze.Enum;

namespace Catze
{
    /// <summary>
    /// Tower의 스크립터블 오브젝트
    /// </summary>
    [CreateAssetMenu(fileName = "SO_Tower", menuName = "SO/RTD/Tower", order = 0)]
    public class SO_Tower : ScriptableObject
    {
        [SerializeField] protected Influence _influence;
        [SerializeField] protected SO_BuildTierInfo _buildTierInfo;
        [SerializeField] protected int _atkDamage;
        [SerializeField] protected int _increaseLevelAtkDamage;
        [SerializeField] protected float _atkSpeed;
        [SerializeField] protected int _atkRange;
        [SerializeField] protected Sprite _sptTower;
        [SerializeField] protected GameObject _pfTowerModel;
        [SerializeField] protected Projectile _pfProjectile;
        [SerializeField] protected Tower _pfTower;
        [SerializeField] protected float _atkCrtRate;
        [SerializeField] protected int _atkCrtDamage;
        [SerializeField] protected List<int> _skillIds;
        [SerializeField] protected AudioClip _towerAttackClip;
        [SerializeField] protected SO_TAnimation _soTowerAnimation;

        public SO_BuildTierInfo BuildTierInfo => _buildTierInfo;

        public int Id => 100000 + InfluenceInt + (int)_buildTierInfo.Tier;
        public Influence Influence => _influence;
        public int InfluenceInt => (int)_influence;
        public TowerTier Tier => _buildTierInfo.Tier;
        public string DisplayName => $"{_influence} {_buildTierInfo.Tier}";
        public int AtkDamage => _atkDamage;
        public int IncreaseLevelAtkDamage => _increaseLevelAtkDamage;
        public float AtkSpeed => _atkSpeed;
        public int AtkRange => _atkRange;
        public bool IsAtkSplash => _buildTierInfo.Tier == TowerTier.Eternity;
        public Sprite SptTower => _sptTower;
        public GameObject PfTowerModel=> _pfTowerModel;
        public Projectile PfProjectile => _pfProjectile;
        public Tower PfTower => _pfTower;
        public float AtkCrtRate => _atkCrtRate;
        public int AtkCrtDamage => _atkCrtDamage;
        public List<int> SkillIds => _skillIds;
        public bool CanAcend => _buildTierInfo.Tier != TowerTier.Eternity;
        public bool CanSell => _buildTierInfo.Tier != TowerTier.Eternity;
        public int SellCost => _buildTierInfo.SellGold;
        public SO_TAnimation SOTAnimation => _soTowerAnimation;
        public float AcendRate => _buildTierInfo.BuildWeightRate;
        public int AcendCost => _buildTierInfo.Cost;
        public GameObject PfGradeColor => _buildTierInfo.PfGradeColor;
        public AudioClip TowerAttackClip => _towerAttackClip;
    }
}