﻿using Catze.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;
    public static Action<int, float> OnEnemyHiyRecordEvent;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHealth { get; set; }
    
    private Image _healthBar;
    private Enemy _enemy;
    private EnemyFX _enemyFX;
    
    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        _enemy = GetComponent<Enemy>();
        _enemyFX = GetComponent<EnemyFX>();
    }

    private void Update()
    {
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount,CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void TryDealDamage(float damageReceived)
    {
        if (API.Instance.Replay.IsReplayMode) return;

        DealDamage(damageReceived);
    }

    public void DealDamage(float damageReceived)
    {
        OnEnemyHiyRecordEvent?.Invoke(_enemy.Id, damageReceived);

        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
            OnEnemyHit?.Invoke(_enemy);
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1f;
    }
    
    private void Die()
    {/*
        AchievementManager.Instance.AddProgress("Kill20", 1);
        AchievementManager.Instance.AddProgress("Kill50", 1);
        AchievementManager.Instance.AddProgress("Kill100", 1);*/
        OnEnemyKilled?.Invoke(_enemy);
    }
}
