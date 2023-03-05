using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class ItemManager : MUnit<ItemManager>
    {
        [Header("Items")]
        [SerializeField] private List<SO_Item> _soitemList = new List<SO_Item>();

        // HashSet�� ����Ͽ� �ߺ� ���� ���� ���� ������ ������ ���
        private readonly HashSet<Item> _itemSet = new HashSet<Item>();

        // ������ ID�� Ű�� ����Ͽ� �������� ������ �˻��� �� �ִ� Dictionary
        private readonly Dictionary<int, Item> _itemDict = new Dictionary<int, Item>();

        #region Item Pooling
        private Queue<Item> _itemPool;

        // ��ü Ǯ �ʱ�ȭ
        private void InitializePool()
        {
            _itemPool = _itemPool ?? new Queue<Item>();
        }

        // ���ο� ������ ��ü ���� �Ǵ� ��ü Ǯ���� ������ �����ϴ� �޼���
        private Item CreateItem(SO_Item soItem)
        {
            InitializePool();

            if (_itemPool.Count > 0)
            {
                var item = _itemPool.Dequeue();
                item.Reset(soItem);
                return item;
            }
            else
            {
                return new Item(soItem);
            }
        }

        // ������ ��ü Ǯ�� ��ȯ�ϴ� �޼���
        private void ReturnToPool(Item item)
        {
            InitializePool();

            _itemPool.Enqueue(item);
        }
        #endregion

        // 0�� 1�� ����� ����
        private const int ZERO = 0;
        private const int ONE = 1;

        // ���׷��̵� ȭ�� �������� ������ ��ȯ�ϴ� �޼���
        public int GetUpgradeCurrency()
        {
            var upgradeCurrency = _itemDict.GetValueOrDefault(0);
            return upgradeCurrency?.StackCount ?? ZERO;
        }

        // �������� �߰��ϰų� ������ ������Ű�� �޼���
        public void AddOrIncrease(int itemId, int amount = ONE)
        {
            if (amount == ZERO)
            {
                return;
            }

            // ������ ID�� SO_Item�� ã��
            SO_Item soItem = _soitemList.Find(x => x.ItemId == itemId);

            if (soItem == null)
            {
                LogError("�������� �������� �ʽ��ϴ�.");
                return;
            }

            // ������ ID�� ������ ������ �������� �˻�
            Item item = _itemDict.GetValueOrDefault(itemId);

            if (item == null)
            {
                // ���ο� �������� �����ϰ� ����Ʈ�� ��ųʸ��� �߰�
                item = CreateItem(soItem);
                if (amount > ONE)
                {
                    item.IncreaseStackCount(amount - ONE);
                }
                _itemSet.Add(item);
                _itemDict.Add(itemId, item);
            }
            else
            {
                // ������ ������ �������� ���� ���� ������Ŵ
                if (item.IsStackable(amount))
                {
                    item.IncreaseStackCount(amount);
                }
                else
                {
                    Log($"{amount}, {item.StackCount}, {amount + item.StackCount}, {item.MaxStackCount}");
                }
            }
        }

        // �������� �����ϰų� ������ ���ҽ�Ű�� �޼���
        public void RemoveOrDecrease(int itemId, int amount = ONE)
        {
            // ������ ID�� ������ ������ �������� �˻�
            Item item = _itemDict.GetValueOrDefault(itemId);

            if (item == null)
            {
                Log($"���� �������� �����ϴ�");
                return;
            }

            // �������� ���� ���� ���ҽ�Ŵ
            int remain = item.StackCount - amount;

            if (remain >= ZERO)
            {
                if (remain == ZERO)
                {
                    // ������ 0�� �Ǹ� ����Ʈ�� ��ųʸ����� �����ϰ� ��ü Ǯ�� ��ȯ
                    _itemSet.Remove(item);
                    _itemDict.Remove(itemId);
                    ReturnToPool(item);
                }
                else
                {
                    item.DecreaseStackCount(amount);
                }
            }

            else
            {
                Log($"���� �������� ����({item.StackCount})�� amount({amount})���� �����ϴ�. ������ ����ó�� �մϴ�.");

                // ������ ������ �Ǹ� ����Ʈ�� ��ųʸ����� �����ϰ� ��ü Ǯ�� ��ȯ
                _itemSet.Remove(item);
                _itemDict.Remove(itemId);
                ReturnToPool(item);
            }
        }
    }
}
