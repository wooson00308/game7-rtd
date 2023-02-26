using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// 몬스터 유닛 (보스포함)
    /// </summary>
    public class Monster : Unit
    {
        [SerializeField] protected SO_Monster _soMonster;
        public SO_Monster SOMonster => _soMonster;

        protected override void Awake()
        {
            base.Awake();

            SetUnitInfo(_soMonster.Id, _soMonster.DisplayName);
        }
    }
}