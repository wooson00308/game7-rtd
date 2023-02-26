using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName ="SO_Ship", menuName = "SO/RTD/Ship", order = 0)]
    public class SO_Ship : ScriptableObject
    {
        [SerializeField] protected int _id;
        [SerializeField] protected string _displayName;
        [SerializeField] protected GameObject _pfShipModel;
        [SerializeField] protected Ship _pfShip;

        public int Id => _id;
        public string DisplayName => _displayName;
        public GameObject PfShipModel => _pfShipModel;
        public Ship PfShip => _pfShip;
    }
}