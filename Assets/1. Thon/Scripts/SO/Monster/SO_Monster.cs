using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SO_Monster", menuName = "SO/RTD/Monster", order = 0)]
public class SO_Monster : ScriptableObject
{
    [SerializeField] protected int _id;
    [SerializeField] protected int _influence;
    [SerializeField] protected MonsterType _monsterType;
    [SerializeField] protected string _displayName;

    public int Id => _id;
    public int Influence => _influence;
    public MonsterType MonsterType => _monsterType;
    public string DisplayName => _displayName;
}
