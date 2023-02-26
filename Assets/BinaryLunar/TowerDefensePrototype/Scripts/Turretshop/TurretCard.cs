using Catze.API;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ReplayData;

public class TurretCard : MonoBehaviour
{
    int _index;
    public int Index 
    {
        get => _index;
        set => _index = value;
    }

    public static Action<TurretSettings> OnPlaceTurretRecordEvent;
    public static Action<TurretSettings> OnPlaceTurret;
    
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public TurretSettings TurretLoaded { get; set; }
    
    public void SetupTurretButton(TurretSettings turretSettings)
    {
        TurretLoaded = turretSettings;
        turretImage.sprite = turretSettings.TurretShopSprite;
        turretCost.text = turretSettings.TurretShopCost.ToString();
    }

    public void TryPlaceTurret()
    {
        if (API.Instance.Replay.IsReplayMode) return;

        PlaceTurret();
    }

    public void PlaceTurret()
    {
        if (CurrencySystem.Instance.TotalCoins >= TurretLoaded.TurretShopCost)
        {
            OnPlaceTurretRecordEvent?.Invoke(TurretLoaded);

            CurrencySystem.Instance.RemoveCoins(TurretLoaded.TurretShopCost);
            UIManager.Instance.CloseTurretShopPanel();
            OnPlaceTurret?.Invoke(TurretLoaded);
        }
    }
}
