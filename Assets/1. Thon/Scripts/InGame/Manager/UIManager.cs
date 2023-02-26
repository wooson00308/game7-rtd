using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class TowerInfoPopupParam
    {
        public Node node;
        public SO_Tower tower;
    }

    /// <summary>
    /// UI 관련 기능들을 관리한다.
    /// </summary>
    public class UIManager : MUnit<UIManager>
    {
        [SerializeField] private TowerInfoPopupUI _popupInfoUnit;

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

        public void HideTowerInfo()
        {
            _popupInfoUnit.SetActive(false);
        }

        public void HideAllPopup()
        {
            _popupInfoUnit.SetActive(false);
        }
    }
}