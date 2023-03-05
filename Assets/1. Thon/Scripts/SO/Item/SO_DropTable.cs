using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "DropTable", menuName = "SO/RTD/Item/DropTable", order = 1)]
    public class SO_DropTable : ScriptableObject
    {
        [Serializable]
        public class Drop
        {
            public Effect _pfDropFx;
            public SO_Item _item;
            public int _dropRate;
            public int _dropCount;
        }

        [SerializeField] private List<Drop> _dropItems;
        [SerializeField] private int _dropMoney;

        public List<Drop> DropItems => _dropItems;
        public int DropMoney => _dropMoney;
    }
}