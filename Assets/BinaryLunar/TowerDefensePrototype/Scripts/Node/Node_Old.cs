using Catze.API;
using System;
using UnityEngine;
using static ReplayData;

public class Node_Old : MonoBehaviour
{
    #region Custom Fields 

    int _index;
    public int Index 
    {
        get => _index;
        set => _index = value;
    }

    #endregion
    public static Action OnTurretSoldRecordEvent;
    public static Action<Node_Old> OnNodeSelected;
    public static Action OnTurretSold;

    [SerializeField] private GameObject attackRangeSprite;
    
    public Turret Turret { get; set; }

    private float _rangeSize;
    private Vector3 _rangeOriginalSize;

    private void Start()
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }
    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }
    public bool IsEmpty()
    {
        return Turret == null;
    }
    public void CloseAttackRangeSprite()
    {
        attackRangeSprite.SetActive(false);
    }
    public void SelectTurret()
    {
        if (Replayer.IsReplayMode) return;

        OnNodeSelected?.Invoke(this);
        if (!IsEmpty())
        {
            ShowTurretInfo();
        }
    }
    public void TrySellTurret()
    {
        if (API.Inst.Replay.IsReplayMode) return;

        SellTurret();
    }

    public void SellTurret()
    {
        if (!IsEmpty())
        {
            OnTurretSoldRecordEvent?.Invoke();
            CurrencySystem.Instance.AddCoins(Turret.TurretUpgrade.GetSellValue());
            Destroy(Turret.gameObject);
            Turret = null;
            attackRangeSprite.SetActive(false);
            OnTurretSold?.Invoke();
        }
    }

    private void ShowTurretInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = _rangeOriginalSize * Turret.AttackRange / (_rangeSize / 2);
    }
}
