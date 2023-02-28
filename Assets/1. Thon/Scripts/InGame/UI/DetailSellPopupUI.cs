using Catze.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Catze
{
    public class DetailSellPopupUI : PopupUI
    {
        static readonly Color s_selectedColor = new Color(.5f, .5f, .5f);
        static readonly Color s_deselectedColor = new Color(1, 1, 1);
        [Serializable]
        public class DetailButtonUI
        {
            [SerializeField] bool isSelected;
            public bool IsSelected => isSelected;

            public Button button;
            Image image;

            public void Toggle()
            {
                isSelected = !isSelected;

                SetImage();
            }

            public void SetImage()
            {
                if (image == null)
                {
                    image = button.GetComponent<Image>();
                }

                image.color = isSelected ? s_selectedColor : s_deselectedColor;
            }
        }

        [Header("DetailSellPopupUI")]

        [SerializeField] DetailButtonUI _nifeInfluence;
        [SerializeField] DetailButtonUI _gunInfluence;
        [SerializeField] DetailButtonUI _lanceInfluence;

        [Space]

        [SerializeField] DetailButtonUI _commonTier;
        [SerializeField] DetailButtonUI _rareTier;
        [SerializeField] DetailButtonUI _heroicTier;
        [SerializeField] DetailButtonUI _legendTier;
        
        [Space]
        
        [SerializeField] Button _sellButton;

        Dictionary<Influence, DetailButtonUI> _influenceButtons = new Dictionary<Influence, DetailButtonUI>();
        Dictionary<TowerTier, DetailButtonUI> _tierButtons = new Dictionary<TowerTier, DetailButtonUI>();


        protected override void Awake()
        {
            base.Awake();

            _influenceButtons.Add(Influence.Nife, _nifeInfluence);
            _influenceButtons.Add(Influence.Gun, _gunInfluence);
            _influenceButtons.Add(Influence.Lance, _lanceInfluence);

            _tierButtons.Add(TowerTier.Common, _commonTier);
            _tierButtons.Add(TowerTier.Rare, _rareTier);
            _tierButtons.Add(TowerTier.Heroic, _heroicTier);
            _tierButtons.Add(TowerTier.Legend, _legendTier);

            _sellButton.onClick.AddListener(() => BulkSell());

            foreach (var pair in _influenceButtons)
            {
                var value = pair.Value;
                value.SetImage();

                var btn = value.button;
                btn.onClick.AddListener(() => ToggleInfluenceButton(pair.Key));
            }

            foreach (var pair in _tierButtons)
            {
                var value = pair.Value;
                value.SetImage();

                var btn = pair.Value.button;
                btn.onClick.AddListener(() => ToggleTierButton(pair.Key));
            }

            gameObject.SetActive(false);
        }

        void ToggleInfluenceButton(Influence id)
        {
            var toggleBtn = _influenceButtons[id];

            bool canToggle = true;

            if(toggleBtn.IsSelected)
            {
                canToggle = false;

                foreach (var pair in _influenceButtons)
                {
                    if (pair.Key.Equals(id)) continue;

                    var detailBtn = pair.Value;
                    if (detailBtn.IsSelected) canToggle = true;
                }
            }

            if (canToggle)
            {
                toggleBtn.Toggle();
            }
        }

        void ToggleTierButton(TowerTier tier)
        {
            var toggleBtn = _tierButtons[tier];

            bool canToggle = true;

            if (toggleBtn.IsSelected)
            {
                canToggle = false;

                foreach (var pair in _tierButtons)
                {
                    if (pair.Key.Equals(tier)) continue;

                    var detailBtn = pair.Value;
                    if (detailBtn.IsSelected) canToggle = true;
                }
            }

            if (canToggle)
            {
                toggleBtn.Toggle();
            }
        }

        void BulkSell()
        {
            List<Influence> influences = new List<Influence>();
            List<TowerTier> tiers = new List<TowerTier>();

            foreach(var inf in _influenceButtons)
            {
                var value = inf.Value;
                if(value.IsSelected)
                {
                    influences.Add(inf.Key);
                }
            }

            foreach (var tier in _tierButtons)
            {
                var value = tier.Value;
                if (value.IsSelected)
                {
                    tiers.Add(tier.Key);
                }
            }

            TowerManager.Instance.BulkSell(influences, tiers);
        }
    }
}