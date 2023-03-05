using Catze.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "New Item", menuName = "SO/RTD/Item")]
    public class SO_Item : ScriptableObject
    {
        [SerializeField] private int _itemId;          // ������ ID
        [SerializeField] private string _itemName;          // ������ �̸�
        [SerializeField] private string _itemDescription;   // ������ ����
        [SerializeField] private Sprite _itemIcon;          // ������ ������
        [SerializeField] private ItemType _itemType;        // ������ Ÿ��
        [SerializeField] private int _itemPrice;            // ������ ����
        [SerializeField] private int _maxStackCount;        // �ִ� ���� ����

        public int ItemId => _itemId;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public Sprite ItemIcon => _itemIcon;
        public ItemType ItemType => _itemType;
        public int ItemPrice => _itemPrice;
        public int MaxStackCount => _maxStackCount;
    }
}
