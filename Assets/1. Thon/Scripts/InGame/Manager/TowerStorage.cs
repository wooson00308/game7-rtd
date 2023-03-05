using Catze.Enum;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catze
{
    public class TowerStorage : MUnit<TowerStorage>
    {
        [SerializeField] CountTowerUI _countTowerUI;

        private List<Tower> _buildTowers = new List<Tower>();
        private Dictionary<Influence, List<Tower>> _influenceTowers = new Dictionary<Influence, List<Tower>>();
        private Dictionary<TowerTier, List<Tower>> _tierTowers = new Dictionary<TowerTier, List<Tower>>();

        public List<Tower> BuildTowers => _buildTowers;

        protected override void Awake()
        {
            base.Awake();

            foreach (Influence influence in System.Enum.GetValues(typeof(Influence)))
            {
                _influenceTowers.Add(influence, new List<Tower>());
            }

            foreach (TowerTier tier in System.Enum.GetValues(typeof(TowerTier)))
            {
                _tierTowers.Add(tier, new List<Tower>());
            }

            _countTowerUI.ChangeUI();
        }

        public int GetInfluenceCount(Influence influence) => _influenceTowers[influence].Count;
        public int GetTierCount(TowerTier tier) => _tierTowers[tier].Count;
        public int GetInfluenceCount_FilterTier(Influence influence, TowerTier tier) => _influenceTowers[influence].Count(tower => tower.SOTower.Tier.Equals(tier));

        public void Add(Tower tower)
        {
            _buildTowers.Add(tower);
            _influenceTowers[tower.SOTower.Influence].Add(tower);
            _tierTowers[tower.SOTower.Tier].Add(tower);

            _countTowerUI.ChangeUI();
        }

        public void Remove(Tower tower)
        {
            _buildTowers.Remove(tower);
            _influenceTowers[tower.SOTower.Influence].Remove(tower);
            _tierTowers[tower.SOTower.Tier].Remove(tower);

            _countTowerUI.ChangeUI();
        }
    }
}
