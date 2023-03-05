using Catze.Enum;
using System;
using UnityEngine;

namespace Catze
{
    [Serializable]
    public class Item
    {
        private int _itemId;
        private string _itemName;         // ������ �̸�
        private string _itemDescription;  // ������ ����
        private Sprite _itemIcon;         // ������ ������
        private ItemType _itemType;       // ������ Ÿ��
        private int _itemPrice;           // ������ ����
        private int _stackCount;          // ���� ����
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

        // ������ ����
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
