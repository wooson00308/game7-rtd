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
        [SerializeField] protected TowerTier _tier;
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
        [SerializeField] protected int _sellGold;
        [SerializeField] protected float _ascendRate;
        [SerializeField] protected SO_TAnimation _soTowerAnimation;
        [SerializeField] protected SO_BuildTierInfo _buildTierInfo;

        public int Id => 100000 + InfluenceInt + (int)_tier;
        public Influence Influence => _influence;
        public int InfluenceInt => (int)_influence;
        public TowerTier Tier => _tier;
        public string DisplayName => $"{_influence} {_tier}";
        public int AtkDamage => _atkDamage;
        public int IncreaseLevelAtkDamage => _increaseLevelAtkDamage;
        public float AtkSpeed => _atkSpeed;
        public int AtkRange => _atkRange;
        public bool IsAtkSplash => _tier == TowerTier.Eternity;
        public Sprite SptTower => _sptTower;
        public GameObject PfTowerModel=> _pfTowerModel;
        public Projectile PfProjectile => _pfProjectile;
        public Tower PfTower => _pfTower;
        public float AtkCrtRate => _atkCrtRate;
        public int AtkCrtDamage => _atkCrtDamage;
        public List<int> SkillIds => _skillIds;
        public bool CanAcend => _tier != TowerTier.Eternity;
        public bool CanSell => _tier != TowerTier.Eternity;
        public int SellCost => _sellGold;
        public float AcendRate => _ascendRate;
        public SO_TAnimation SOTAnimation => _soTowerAnimation;

        public int AcendCost => _buildTierInfo.Cost;
    }
}