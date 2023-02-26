using Catze.API;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ReplayData;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private RectTransform turretShopPanel;
    [SerializeField] private RectTransform nodeUIPanel;
    [SerializeField] private RectTransform achievementPanel;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private RectTransform winPanel;

    [Header("Text")] 
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;

    [SerializeField] private GameObject _cantTouchUI;

    [SerializeField] private GameObject _replayText;
    private Node_Old _currentNodeSelected;

    private void Update()
    {
        _replayText.SetActive(Replayer.IsReplayMode);

        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = "Wave "+LevelManager.Instance.CurrentWave;
    }

    public void RemoveReplayData()
    {
        _cantTouchUI.SetActive(true);

        API.Inst.PlayFab.DataPart.RemoveReplayData(callback =>
        {
            if (callback == true)
            {
                API.Inst.PlayFab.DataPart.Log("Remove Complete!");
            }
            else
            {
                API.Inst.PlayFab.DataPart.LogError("Remove Failed!");
            }

            _cantTouchUI.SetActive(false);

            SceneManager.LoadScene("TowerDefense");
        });
    }

    public void SlowTime()
    {
        Time.timeScale = 0.5f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void FastTime()
    {
        Time.timeScale = 2f;
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.transform.localScale = Vector3.one;
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
    }

    public void ShowWinPanel()
    {
        winPanel.transform.localScale = Vector3.one;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void OpenAchievementPanel(bool status)
    {
        achievementPanel.gameObject.SetActive(status);

        Vector3 scale = status ? Vector3.one : Vector3.zero;
        achievementPanel.transform.localScale = scale;
    }
    
    public void CloseTurretShopPanel()
    {
        turretShopPanel.transform.localScale = Vector3.zero;
    }

    public void CloseNodeUIPanel()
    {
        _currentNodeSelected.CloseAttackRangeSprite();
        nodeUIPanel.transform.localScale = Vector3.zero;
    }

    public void UpdateUI()
    {
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }

    public void UpgradeTurret()
    {
        _currentNodeSelected.Turret.TurretUpgrade.TryUpgradeTurret();
    }

    public void SellTurret()
    {
        _currentNodeSelected.TrySellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.transform.localScale = Vector3.zero;
    }
    
    private void ShowNodeUI()
    {
        nodeUIPanel.transform.localScale = Vector3.one;
        UpdateUI();
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }

    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }
    
    public void NodeSelected(Node_Old nodeSelected)
    {
        _currentNodeSelected = nodeSelected;

        if (Replayer.IsReplayMode) return;

        if (_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.transform.localScale = Vector3.one;
        }
        else
        {
            ShowNodeUI();
        }
    }
    
    private void OnEnable()
    {
        Node_Old.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node_Old.OnNodeSelected -= NodeSelected;
    }
}
