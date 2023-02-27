using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    public class TowerInfoPopupParam
    {
        public Node node;
        public SO_Tower soTower;
        public Tower tower;
    }

    /// <summary>
    /// UI 관련 기능들을 관리한다.
    /// </summary>
    public class UIManager : MUnit<UIManager>
    {
        [Header("UI Manager")]
        [SerializeField] private TowerInfoPopupUI _popupInfoUnit;
        [SerializeField] private GameObject _upgradeTowerUI;
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private GameObject _gameClearUI;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _spawnTowerCostText;
        [SerializeField] private TMP_Text _timerText;

        public void SetTimer(float time)
        {
            _timerText.text = Util.GetTimerFormat(time);
        }
 
        public void SetSpawnTowerCost(int cost)
        {
            _spawnTowerCostText.text = $"{cost}";
        }

        public void SetMoney(int money)
        {
            _moneyText.text = $"{money}";
        }

        public void ShowTowerInfo(TowerInfoPopupParam towerInfo)
        {
            bool isSameNode = towerInfo.node.Id == _popupInfoUnit.NodeId;
            if (isSameNode && _popupInfoUnit.ActiveSelf)
            {
                _popupInfoUnit.SetActive(false);
                return;
            }

            _popupInfoUnit.SetActive(true, towerInfo);
        }

        public void ShowUpgradeTower()
        {
            _upgradeTowerUI.SetActive(true);
        }

        public void HideUpgradeTower()
        {
            _upgradeTowerUI.SetActive(false);
        }

        public void HideTowerInfo()
        {
            _popupInfoUnit.SetActive(false);
        }

        public void HideAllPopup()
        {
            HideTowerInfo();
            HideUpgradeTower();
            HideGameClearUI();
            HideGameOverUI();
        }

        public void ShowGameClearUI()
        {
            HideAllPopup();
            _gameClearUI.SetActive(true);
        }

        public void HideGameClearUI()
        {
            _gameClearUI.SetActive(false);
        }

        public void ShowGameOverUI()
        {
            HideAllPopup();
            _gameOverUI.SetActive(true);
        }

        public void HideGameOverUI()
        {
            _gameOverUI.SetActive(false);
        }
    }
}