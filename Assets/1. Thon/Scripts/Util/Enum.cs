using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.Enum
{
    public enum IngameEndType
    {
        GameOver,
        GameClear
    }

    public enum Influence
    {
        Nife = 100,
        Gun = 200,
        Lance = 300,
    }

    public enum TowerTier
    {
        Common,
        Rare,
        Heroic,
        Legend,
        Acient,
        Relic,
        Myth,
        Eternity = 8,
    }

    public enum TowerAniState
    {
        Idle,
        Attack,
    }

    public enum MonsterType
    {
        Monster,
        Boss,
    }

    public enum MonsterAniState
    {
        Idle,
        Move,
        Damaged,
        Death,
    }
}