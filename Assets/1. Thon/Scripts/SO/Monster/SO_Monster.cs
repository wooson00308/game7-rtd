using Catze.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    [CreateAssetMenu(fileName = "SO_Monster", menuName = "SO/RTD/Monster", order = 0)]
    public class SO_Monster : ScriptableObject
    {
        [SerializeField] protected int _id;
        [SerializeField] protected int _influence;
        [SerializeField] protected float _delayRecoveryTime;
        [SerializeField] protected int _recoverySpeed;
        [SerializeField] protected MonsterType _monsterType;
        [SerializeField] protected string _displayName;
        [SerializeField] protected Sprite _sptMonster;
        [SerializeField] protected GameObject _pfMonsterModel;
        [SerializeField] protected GameObject _pfMonsterDamagedFx;
        [SerializeField] protected Monster _pfMonster;
        [SerializeField] protected AudioClip _monsterHitClip;
        [SerializeField] protected AudioClip _monsterDeathClip;
        [SerializeField] protected GameObject _pfDamagePopup;

        public int Id => _id;
        public int Influence => _influence;
        public float DelayRecoveryTime => _delayRecoveryTime;
        public int RecoverySpeed => _recoverySpeed;
        public MonsterType MonsterType => _monsterType;
        public string DisplayName => _displayName;
        public Sprite SptMonster => _sptMonster;
        public GameObject PfMonsterModel => _pfMonsterModel;
        public GameObject PfMonsterDamagedFx => _pfMonsterDamagedFx;
        public Monster PfMonster => _pfMonster;
        public AudioClip MonsterHitClip => _monsterHitClip;
        public AudioClip MonsterDeathClip => _monsterDeathClip;
        public GameObject PfDamagePopup => _pfDamagePopup;
    }
}