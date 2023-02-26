using System.Collections.Generic;
using UnityEngine;
using Catze.Enum;

namespace Catze
{
    /// <summary>
    /// Tower의 스크립터블 오브젝트
    /// </summary>
    [CreateAssetMenu(fileName ="SO_Tower", menuName ="SO/RTD/Tower", order = 0)]
    public class SO_Tower : ScriptableObject
    {
        [SerializeField] protected int _id;
        [SerializeField] protected int _influence;
        [SerializeField] protected TowerTier _tier;
        [SerializeField] protected string _displayName;
        [SerializeField] protected int _atkDamage;
        [SerializeField] protected int _increaseLevelAtkDamage;
        [SerializeField] protected float _atkSpeed;
        [SerializeField] protected int _atkRange;
        [SerializeField] protected int _atkSplash;
        [SerializeField] protected Sprite _sptTower;
        [SerializeField] protected GameObject _pfTowerModel;
        [SerializeField] protected GameObject _pfProjectile;
        [SerializeField] protected Tower _pfTower;
        [SerializeField] protected int _atkCrtRate;
        [SerializeField] protected int _atkCrtDamage;
        [SerializeField] protected List<int> _skillIds;
        [SerializeField] protected bool _canLevelUp;
        [SerializeField] protected bool _canSell;
        [SerializeField] protected int _sellGold;
        [SerializeField] protected SO_TAnimation _soTowerAnimation;
 
        public int Id => _id;
        public int Influence => _influence;
        public TowerTier Tier => _tier;
        public string DisplayName => _displayName;
        public int AtkDamage => _atkDamage;
        public int IncreaseLevelAtkDamage => _increaseLevelAtkDamage;
        public float AtkSpeed => _atkSpeed;
        public int AtkRange => _atkRange;
        public int AtkSplash => _atkSplash;
        public Sprite SptTower => _sptTower;
        public GameObject PfTowerModel=> _pfTowerModel;
        public GameObject PfProjectile => _pfProjectile;
        public Tower PfTower => _pfTower;
        public int AtkCrtRate => _atkCrtRate;
        public int AtkCrtDamage => _atkCrtDamage;
        public List<int> SkillIds => _skillIds;
        public bool CanLevelUp => _canLevelUp;
        public bool CanSell => _canSell;
        public int SellGold => _sellGold;
        public SO_TAnimation SOTAnimation => _soTowerAnimation;
    }
}