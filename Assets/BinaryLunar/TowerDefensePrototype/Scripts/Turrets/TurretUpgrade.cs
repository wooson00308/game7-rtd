using Catze.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    public static Action OnUpgradeTurretRecordEvent;
    public static Action OnUpgradeTurret;

    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;

    [Header("Sell")] 
    [Range(0,1)]
    [SerializeField] private float sellPert;
    
    public float SellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }
    
    private TurretProjectile _turretProjectile;
    
    private void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = upgradeInitialCost;

        SellPerc = sellPert;
        Level = 1;
    }
    
    public void TryUpgradeTurret()
    {
        if (API.Inst.Replay.IsReplayMode) return;

        UpgradeTurret();
    }

    public void UpgradeTurret()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            OnUpgradeTurretRecordEvent?.Invoke();

            _turretProjectile.Damage += damageIncremental;
            _turretProjectile.DelayPerShot -= delayReduce;
            UpdateUpgrade();

            UIManager.Instance.UpdateUI();
            OnUpgradeTurret?.Invoke();
        }
    }

    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }
    
    private void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
}
