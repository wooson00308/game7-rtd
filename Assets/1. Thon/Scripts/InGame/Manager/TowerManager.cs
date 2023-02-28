using Catze.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    /// <summary>
    /// 타워 생성 및 판매, 업그레이드 등 타워 관련 기능들을 관리한다.
    /// </summary>
    public class TowerManager : MUnit<TowerManager>
    {
        [Header("Tower Manager")]
        [SerializeField] private Transform _towerParent; 
        [SerializeField] private SO_Ship _soShip;

        [SerializeField] private SO_TowerBuild _soTowerBuild;

        [Serializable]
        public class TowerUpgrade
        {
            public int _influence;

            [Header("UI")]
            public TMP_Text _towerUpgradeCostText;
            public TMP_Text _towerUpgradeLevelText;

            public Button _upgradeButton;

            [Header("Status")]
            public int _cost;
            public int _levelPerCost;
            public int _level;
        }

        [Space]

        [SerializeField] private List<TowerUpgrade> _towerUpgrades;
        [SerializeField] private int _maxUpgradeLevel;

        [Space]

        [SerializeField] private int _startMoney;

        private int _money;
        [SerializeField] private int _spawnTowerCost;

        private SO_BuildTierInfo _buildTierInfo;

        public int Money => _money;

        private Ship _ship;

        public Ship Ship => _ship;

        private List<Tower> _buildTowers = new List<Tower>();

        public void AddTower(Tower tower)
        {
            _buildTowers.Add(tower);
        }

        public void RemoveTower(Tower tower)
        {
            _buildTowers.Remove(tower);
        }

        void GamePrepareEvent()
        {
            _ship = Instantiate(_soShip.PfShip, _towerParent);

            UIManager.Instance.ShowUpgradeTower();

            foreach(var upgrade in _towerUpgrades)
            {
                upgrade._upgradeButton.onClick.AddListener(() => UpgradeTower(upgrade));
                upgrade._towerUpgradeCostText.text = $"{upgrade._cost}";
                upgrade._towerUpgradeLevelText.text = $"Lv. {upgrade._level}";
            }

            UIManager.Instance.HideUpgradeTower();

            _money = _startMoney;

            UIManager.Instance.SetMoney(_money);
            UIManager.Instance.SetSpawnTowerCost(_spawnTowerCost);
        }

        void GameStartEvent()
        {
            Activate();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            UIManager.Instance.SetActiveSpawnButton(!_ship.NodePart.IsNotEmptyNodes());
        }

        public void TryBuildTower()
        {
            if(_ship.NodePart.IsNotEmptyNodes())
            {
                return;    
            }
            
            if(_money < _spawnTowerCost)
            {
                return;
            }

            _money -= _spawnTowerCost;
            UIManager.Instance.SetMoney(_money);

            var soTower = GetRandomSOTower();
            if(soTower != null)
            {
                _ship.NodePart.TryRandomSpawn(soTower);
            }
            else
            {
                Log($"{nameof(TryBuildTower)}, Build Faild!");
            }
        }

        // Return SOTower.PfTower as each BuildWeightRate of SO_TowerBuildTier in _soTowerBuild.
        public SO_Tower GetRandomSOTower()
        {
            var soTowerBuildInfluence = _soTowerBuild.SOTowerBuildInfluences[UnityEngine.Random.Range(0, _soTowerBuild.SOTowerBuildInfluences.Count)];

            float totalWeight = 0;
            foreach (var soTowerBuildInfluenceTier in soTowerBuildInfluence.SOTowerBuildInfuenceTiers)
            {
                totalWeight += soTowerBuildInfluenceTier.SOTierInfo.BuildWeightRate;
            }

            float randomValue = UnityEngine.Random.Range(0, totalWeight);

            foreach (var soTowerBuildTier in soTowerBuildInfluence.SOTowerBuildInfuenceTiers)
            {
                if (randomValue < soTowerBuildTier.SOTierInfo.BuildWeightRate)
                {
                    Log($"{nameof(TryBuildTower)}, Build Success! : {soTowerBuildTier.SOTierInfo.Tier}");
                    _buildTierInfo = soTowerBuildTier.SOTierInfo;
                    return soTowerBuildTier.SOTower;
                }

                randomValue -= soTowerBuildTier.SOTierInfo.BuildWeightRate;
            }

            return null;
        }

        public SO_Tower GetAcendTower(Tower tower)
        {
            if (_money < tower.SOTower.AcendCost)
            {
                Log($"Not Enough Money! : {_money} < {tower.SOTower.AcendCost}");
                return null;
            }

            _money -= tower.SOTower.AcendCost;
            UIManager.Instance.SetMoney(_money);

            var buildInfluenceTier = _soTowerBuild.GetBuildTierInfo(tower.SOTower.Tier);
            if (buildInfluenceTier != null)
            {
                if (Util.GetRateResult(buildInfluenceTier.BuildWeightRate))
                {
                    var buildTier = _soTowerBuild.GetAcendTier(tower.SOTower.Tier);
                    if (buildTier != null)
                    {
                        _buildTierInfo = buildTier.SOTierInfo;
                        return buildTier.SOTower;
                    }
                    
                }
            }

            return null;
        }

        private void UpgradeTower(TowerUpgrade upgrade)
        {
            UIManager.Instance.HideTowerInfo();

            if (upgrade._level >= _maxUpgradeLevel)
            {
                return;
            }

            if(_money < upgrade._cost)
            {
                Log($"Not Enough Money! : {_money} < {upgrade._cost}");
                return;
            }

            _money -= upgrade._cost;
            UIManager.Instance.SetMoney(_money);

            upgrade._cost += upgrade._levelPerCost;
            upgrade._towerUpgradeCostText.text = upgrade._cost.ToString();

            upgrade._level++;
            upgrade._towerUpgradeLevelText.text = $"Lv. {upgrade._level}";

            bool isMaxLevel = upgrade._level >= _maxUpgradeLevel;
            if (isMaxLevel)
            {
                upgrade._upgradeButton.interactable = false;
            }
        }

        public int GetTowerUpgradeLevel(int influence)
        {
            foreach (var _towerUpgrade in _towerUpgrades)
            {
                if (_towerUpgrade._influence == influence)
                {
                    return _towerUpgrade._level;
                }
            }

            LogError($"{nameof(GetTowerUpgradeLevel)}, {influence} is not found. {nameof(TowerUpgrade)}");

            return 0;
        }

        public void AddMoney(int money)
        {
            _money += money;
            UIManager.Instance.SetMoney(_money);
        }

        public void BulkSell(List<Influence> influences, List<TowerTier> tiers)
        {
            List<Tower> towers = new List<Tower>();

            foreach(var tower in _buildTowers)
            {
                bool validInfluence = false;
                foreach(var influence in influences)
                {
                    if (tower.SOTower.Influence.Equals(influence))
                    {
                        validInfluence = true;
                        break;
                    }
                }

                bool validTier = false;
                foreach (var tier in tiers)
                {
                    if (tower.SOTower.Tier.Equals(tier))
                    {
                        validTier = true;
                        break;
                    }
                }

                if (validInfluence && validTier)
                {
                    var node = tower.UpperUnit.GetComponent<Node>();
                    node.SellTower(false);

                    towers.Add(tower);
                }
            }
            
            foreach(var tower in towers)
            {
                RemoveTower(tower);
            }
        }

        protected override void DelayEnable()
        {
            base.DelayEnable();

            GameManager.PrepareState.AddEvent(GamePrepareEvent);
            GameManager.StartState.AddEvent(GameStartEvent);
        }

        protected void OnDisable()
        {
            GameManager.PrepareState.RemoveEvent(GamePrepareEvent);
            GameManager.StartState.RemoveEvent(GameStartEvent);
        }
    }
}