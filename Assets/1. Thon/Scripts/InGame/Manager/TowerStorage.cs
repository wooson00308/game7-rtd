using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Catze
{
    public class TowerStorage : MUnit<TowerStorage>
    {
        List<Tower> _buildTowers = new List<Tower>();
        Dictionary<Influence, List<Tower>> _influenceTowers = new Dictionary<Influence, List<Tower>>();
        Dictionary<TowerTier, List<Tower>> _tierTowers = new Dictionary<TowerTier, List<Tower>>();

        public List<Tower> BuildTowers => _buildTowers;

        [SerializeField] CountTowerUI _countTowerUI;

        protected override void Awake()
        {
            base.Awake();

            foreach(Influence influence in System.Enum.GetValues(typeof(Influence)))
            {
                _influenceTowers.Add(influence, new List<Tower>());
            }

            foreach (TowerTier tier in System.Enum.GetValues(typeof(TowerTier)))
            {
                _tierTowers.Add(tier, new List<Tower>());
            }

            _countTowerUI.ChangeUI();
        }

        public int GetInfluenceCount(Influence influence)
        {
            return _influenceTowers[influence].Count;
        }

        public int GetTierCount(TowerTier tier)
        {
            return _tierTowers[tier].Count;
        }

        public int GetInfluenceCount_FilterTier(Influence influence, TowerTier tier)
        {
            return _influenceTowers[influence].Where(tower => tower.SOTower.Tier.Equals(tier)).Count();
        }

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