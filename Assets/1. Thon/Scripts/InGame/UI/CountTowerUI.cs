using Catze.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Catze
{
    public class CountTowerUI : PopupUI
    {
        [Serializable]
        public class InfluenceTowerUI
        {
            public Influence influence;
            public TMP_Text totalCountText;

            [Serializable]
            public class TierTowerUI
            {
                public TowerTier tier;
                public GameObject tierObject;
            }

            public List<TierTowerUI> _tierTowerUIs;
        }

        [Header("CountTowerUI")]
        [SerializeField] List<InfluenceTowerUI> _influenceTowerUIs;

        public void ChangeUI()
        {
            foreach(var ui in _influenceTowerUIs)
            {
                var influence = ui.influence;
                ui.totalCountText.text = TowerStorage.Instance.GetInfluenceCount(influence).ToString();

                foreach (var tierUI in ui._tierTowerUIs)
                {
                    var tier = tierUI.tier;
                    int cnt = TowerStorage.Instance.GetInfluenceCount_FilterTier(influence, tier);
                    
                    tierUI.tierObject.SetActive(cnt > 0);
                    if(tierUI.tierObject.activeSelf)
                    {
                        var text = tierUI.tierObject.GetComponentInChildren<TMP_Text>();
                        text.text = cnt.ToString();
                    }
                }
            }
        }
    }
}