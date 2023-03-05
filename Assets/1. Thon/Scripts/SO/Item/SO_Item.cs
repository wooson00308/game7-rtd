using Catze.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "New Item", menuName = "SO/RTD/Item")]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private int _itemId;          // 아이템 ID
        [SerializeField] private string _itemName;          // 아이템 이름
        [SerializeField] private string _itemDescription;   // 아이템 설명
        [SerializeField] private Sprite _itemIcon;          // 아이템 아이콘
        [SerializeField] private ItemType _itemType;        // 아이템 타입
        [SerializeField] private int _itemPrice;            // 아이템 가격
        [SerializeField] private int _maxStackCount;        // 최대 스택 개수

        public int ItemId => _itemId;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public Sprite ItemIcon => _itemIcon;
        public ItemType ItemType => _itemType;
        public int ItemPrice => _itemPrice;
        public int MaxStackCount => _maxStackCount;
    }
}
