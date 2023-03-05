using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class TowerInfoPopupParam
    {
        public Node node;
        public SO_Tower soTower;
        public Tower tower;
    }

    public class UIManager : MUnit<UIManager>
    {
        [Header("UI Manager")]
        public Transform DPParent;
        [SerializeField] private TowerInfoPopupUI _popupInfoUnit;
        [SerializeField] private GameObject _upgradeTowerUI;
        [SerializeField] private GameObject _detailSellOptionUI;
        [SerializeField] private GameObject _gameOverUI;
        [SerializeField] private GameObject _gameClearUI;
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _ucText;
        [SerializeField] private TMP_Text _spawnTowerCostText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private GameObject _touchRangeUI;

        public void SetTimer(float time) => _timerText.text = Util.GetTimerFormat(time);

        public void SetSpawnTowerCost(int cost) => _spawnTowerCostText.text = cost.ToString();

        public void SetMoney(int money) => _moneyText.text = money.ToString();

        public void SetUC(int uc) => _ucText.text = uc.ToString();

        public void ShowTowerInfo(TowerInfoPopupParam towerInfo)
        {
            if (_popupInfoUnit.NodeId == towerInfo.node.Id && _popupInfoUnit.ActiveSelf)
            {
                _popupInfoUnit.SetActive(false);
                return;
            }

            _popupInfoUnit.SetActive(true, towerInfo);
        }

        public void ToggleUpgradeTower() => _upgradeTowerUI.SetActive(!_upgradeTowerUI.activeSelf);

        public void ShowUpgradeTower() => _upgradeTowerUI.SetActive(true);

        public void HideUpgradeTower() => _upgradeTowerUI.SetActive(false);

        public void ToggleTowerInfo() => _popupInfoUnit.SetActive(!_popupInfoUnit.ActiveSelf);

        public void HideTowerInfo() => _popupInfoUnit.SetActive(false);

        public void ToggleDetailSellOptionUI() => _detailSellOptionUI.SetActive(!_detailSellOptionUI.activeSelf);

        public void HideDetailSellOptionUI() => _detailSellOptionUI.SetActive(false);

        public void HideAllPopup()
        {
            HideTowerInfo();
            HideUpgradeTower();
            HideGameClearUI();
            HideGameOverUI();
            HideDetailSellOptionUI();
        }

        public void ShowGameClearUI()
        {
            HideAllPopup();
            _gameClearUI.SetActive(true);
        }

        public void HideGameClearUI() => _gameClearUI.SetActive(false);

        public void ShowGameOverUI()
        {
            HideAllPopup();
            _gameOverUI.SetActive(true);
        }

        public void HideGameOverUI() => _gameOverUI.SetActive(false);

        public void SetActiveSpawnButton(bool value) => _spawnButton.interactable = value;

        public void SetActiveTouchRangeUI(bool value) => _touchRangeUI.SetActive(value);

        public void MoveTouchRangeUI(Vector3 position) => _touchRangeUI.transform.position = position;
    }
}
