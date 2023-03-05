using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class MDeathState : Monster.State
    {
        private bool _isDeath;
        public bool IsDeath => _isDeath;
        public Action OnMonsterDeathEvent;

        public override void Activate()
        {
            if (_isDeath) return;
            base.Activate();

            if (Upper.SOMonster.MonsterDeathClip != null)
            {
                SoundManager.Instance.PlaySFX(Upper.SOMonster.MonsterDeathClip);
            }

            // 아이템 드랍
            foreach (var drop in Upper.DropTable.DropItems)
            {
                if (Util.GetRateResult(drop._dropRate))
                {
                    if(drop._pfDropFx != null)
                    {
                        var dropfx = Instantiate(drop._pfDropFx, Upper.transform.position, Quaternion.identity);
                        dropfx.Activate();
                    }

                    ItemManager.Instance.AddOrIncrease(drop._item.ItemId, drop._dropCount);
                    UIManager.Instance.SetUC(ItemManager.Instance.GetUpgradeCurrency());
                }
            }
            
            TowerManager.Instance.AddMoney(Upper.DropTable.DropMoney);

            OnMonsterDeathEvent?.Invoke();
            OnMonsterDeathEvent = () => { };

            if (Upper != null)
            {
                Destroy(Upper.gameObject);
            }
        }
    }
}
