using Catze.Enum;
using System;
using UnityEngine;

namespace Catze
{
    [Serializable]
    public class Item
    {
        private int _itemId;
        private string _itemName;         // 아이템 이름
        private string _itemDescription;  // 아이템 설명
        private Sprite _itemIcon;         // 아이템 아이콘
        private ItemType _itemType;       // 아이템 타입
        private int _itemPrice;           // 아이템 가격
        private int _stackCount;          // 스택 개수
        private int _maxStackCount;

        public int ItemId => _itemId;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public Sprite ItemIcon => _itemIcon;
        public ItemType ItemType => _itemType;
        public int ItemPrice => _itemPrice;
        public int StackCount => _stackCount;
        public int MaxStackCount => _maxStackCount;

        public void IncreaseStackCount(int amount = 1)
        {
            _stackCount += amount;
        }

        public void DecreaseStackCount(int amount = 1)
        {
            _stackCount -= amount;
            if (_stackCount < 0)
            {
                _stackCount = 0;
            }
        }

        public bool IsStackable(int amount = 1)
        {
            return _stackCount + amount < _maxStackCount;
        }

        // 생성자 구현
        public Item(SO_Item item)
        {
            Reset(item);
        }

        // Reset
        public void Reset(SO_Item item)
        {
            _itemName = item.ItemName;
            _itemDescription = item.ItemDescription;
            _itemIcon = item.ItemIcon;
            _itemType = item.ItemType;
            _itemPrice = item.ItemPrice;
            _maxStackCount = item.MaxStackCount;
            _stackCount = 1;
        }
    }
}
