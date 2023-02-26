using Catze;
using Catze.API;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public static Action GameStartEvent;
    public static Action GameOverEvent;
    public static Action GameWinEvent;

    [SerializeField] private int lives = 10;

    public int TotalLives { get; set; }
    public int CurrentWave { get; set; }
    
    private void Start()
    {
        TotalLives = lives;
        CurrentWave = 1;

        TryGameStart();
    }

    void TryGameStart()
    {
        API.Inst.PlayFab.AuthPart.OnLoginWithEmail(new LoginWithEmailModel
        {
            email = ConstantStrings.PLAYFAB_EMAIL,
            password = ConstantStrings.PLAYFAB_PASSWORD
        }, 
        result =>
        {
            API.Inst.PlayFab.AuthPart.Log(result.message);

            if(result.isSuccess)
            {
                API.Inst.Replay.TryActivate();
            }
            else
            {
                // Login Failed Logic
            }
        });

        GameStartEvent?.Invoke();
    }

    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        UIManager.Instance.ShowGameOverPanel();
        UIManager.Instance.PauseTime();

        GameOverEvent?.Invoke();
    }

    private void YouWin()
    {
        UIManager.Instance.ShowWinPanel();
        UIManager.Instance.PauseTime();

        GameWinEvent?.Invoke();
    }

    private void WaveCompleted()
    {
        CurrentWave++;
        if (CurrentWave == 4)
            YouWin();

        AchievementManager.Instance.AddProgress("Waves1", 1);
        AchievementManager.Instance.AddProgress("Waves2", 1);
        AchievementManager.Instance.AddProgress("Waves3", 1);
    }
    
    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
        Spawner.OnWaveCompleted -= WaveCompleted;
    }
}
