using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class ItemManager : MUnit<ItemManager>
    {
        [Header("Items")]
        [SerializeField] private List<SO_Item> _soitemList = new List<SO_Item>();

        // HashSet를 사용하여 중복 없이 빠른 접근 가능한 아이템 목록
        private readonly HashSet<Item> _itemSet = new HashSet<Item>();

        // 아이템 ID를 키로 사용하여 아이템을 빠르게 검색할 수 있는 Dictionary
        private readonly Dictionary<int, Item> _itemDict = new Dictionary<int, Item>();

        #region Item Pooling
        private Queue<Item> _itemPool;

        // 객체 풀 초기화
        private void InitializePool()
        {
            _itemPool = _itemPool ?? new Queue<Item>();
        }

        // 새로운 아이템 객체 생성 또는 객체 풀에서 꺼내서 재사용하는 메서드
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

        // 아이템 객체 풀에 반환하는 메서드
        private void ReturnToPool(Item item)
        {
            InitializePool();

            _itemPool.Enqueue(item);
        }
        #endregion

        // 0과 1을 상수로 정의
        private const int ZERO = 0;
        private const int ONE = 1;

        // 업그레이드 화폐 아이템의 개수를 반환하는 메서드
        public int GetUpgradeCurrency()
        {
            var upgradeCurrency = _itemDict.GetValueOrDefault(0);
            return upgradeCurrency?.StackCount ?? ZERO;
        }

        // 아이템을 추가하거나 개수를 증가시키는 메서드
        public void AddOrIncrease(int itemId, int amount = ONE)
        {
            if (amount == ZERO)
            {
                return;
            }

            // 아이템 ID로 SO_Item을 찾음
            SO_Item soItem = _soitemList.Find(x => x.ItemId == itemId);

            if (soItem == null)
            {
                LogError("아이템이 존재하지 않습니다.");
                return;
            }

            // 아이템 ID로 기존에 보유한 아이템을 검색
            Item item = _itemDict.GetValueOrDefault(itemId);

            if (item == null)
            {
                // 새로운 아이템을 생성하고 리스트와 딕셔너리에 추가
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
                // 기존에 보유한 아이템의 스택 수를 증가시킴
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

        // 아이템을 제거하거나 개수를 감소시키는 메서드
        public void RemoveOrDecrease(int itemId, int amount = ONE)
        {
            // 아이템 ID로 기존에 보유한 아이템을 검색
            Item item = _itemDict.GetValueOrDefault(itemId);

            if (item == null)
            {
                Log($"보유 아이템이 없습니다");
                return;
            }

            // 아이템의 스택 수를 감소시킴
            int remain = item.StackCount - amount;

            if (remain >= ZERO)
            {
                if (remain == ZERO)
                {
                    // 스택이 0이 되면 리스트와 딕셔너리에서 제거하고 객체 풀에 반환
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
                Log($"보유 아이템의 스택({item.StackCount})이 amount({amount})보다 적습니다. 아이템 삭제처리 합니다.");

                // 스택이 음수가 되면 리스트와 딕셔너리에서 제거하고 객체 풀에 반환
                _itemSet.Remove(item);
                _itemDict.Remove(itemId);
                ReturnToPool(item);
            }
        }
    }
}
