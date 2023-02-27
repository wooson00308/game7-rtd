using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class TowerInfoPopupUI : PopupUI
    {
        private int _nodeId;
        public int NodeId => _nodeId;

        [Header("TowerInfoPopupUI")]
        [SerializeField] private Image _displayTowerImage; 
        [SerializeField] private TMP_Text _influenceText;
        [SerializeField] private TMP_Text _gradeText;

        [SerializeField] private TMP_Text _atkDamageText;
        [SerializeField] private TMP_Text _atkSpeedText;
        [SerializeField] private TMP_Text _atkRangeText;
        [SerializeField] private TMP_Text _atkCrtRateText;
        [SerializeField] private TMP_Text _atkCrtDamageText;

        [SerializeField] private Button _sellButton;
        
        [SerializeField] private TMP_Text _sellCostText;
        
        [SerializeField] private Button _acendButton;
        [SerializeField] private TMP_Text _ascendRateText;

        [SerializeField] private TMP_Text _acendCostText;
 
        public bool ActiveSelf => gameObject.activeSelf;

        /// <summary>
        /// 타워 정보를 열람 할 때 팝업되는 UI
        /// </summary>
        /// <param name="value">true 일 시 타워 정보도 같이 담아서 보내야한다.</param>
        /// <param name="param"></param>
        public void SetActive(bool value, TowerInfoPopupParam param = null)
        {
            base.SetActive(value);

            if (!value) return;
            if (param == null)
            {
                LogError($"{nameof(TowerInfoPopupUI)}, {nameof(SetActive)}, tower info is null!");
                return;
            }

            _nodeId = param.node.Id;

            _displayTowerImage.sprite = param.soTower.SptTower;
            _influenceText.text = param.soTower.Influence.ToString();
            _gradeText.text = param.soTower.Tier.ToString();

            _atkDamageText.text = param.tower.AttackPart.AttackDamage.ToString();
            _atkSpeedText.text = param.soTower.AtkSpeed.ToString();
            _atkRangeText.text = param.soTower.AtkRange.ToString();
            _atkCrtRateText.text = param.soTower.AtkCrtRate.ToString();
            _atkCrtDamageText.text = param.soTower.AtkCrtDamage.ToString();

            _acendCostText.text = param.soTower.AcendCost.ToString();

            _sellButton.gameObject.SetActive(param.soTower.CanSell);
            if (param.soTower.CanSell)
            {
                _sellCostText.text = param.soTower.SellCost.ToString();
            }

            _acendButton.gameObject.SetActive(param.soTower.CanAcend);
            if(param.soTower.CanAcend)
            {
                _ascendRateText.text = $"{param.soTower.AcendRate}%";
            }
        }

        public void Deactivate()
        {
            SetActive(false);
        }

        public void Sell()
        {
            var node = TowerManager.Instance.Ship.NodePart.Nodes[_nodeId];
            node.SellTower();

            TowerManager.Instance.Ship.NodePart.DeselectedNode();
            Deactivate();
        }

        public void Acend()
        {
            var node = TowerManager.Instance.Ship.NodePart.Nodes[_nodeId];

            if (node.AcendTower())
            {
                TowerManager.Instance.Ship.NodePart.DeselectedNode();
                Deactivate();
            }
        }
    }
}