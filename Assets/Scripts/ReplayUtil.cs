using System;
using System.IO;
using UnityEngine;

/// <summary>
/// old Scripts
/// </summary>
public class ReplayUtil : MonoBehaviour
{
    static bool _isDebugMode;

    #region Logger 

    public static void OnDebugMode(bool value = true)
    {
        _isDebugMode = value;
    }

    public static void Log(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.Log(obj.ToString());
    }

    public static void LogWarning(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.LogWarning(obj.ToString());
    }

    public static void LogError(object obj)
    {
        if (_isDebugMode == false) return;

        Debug.LogError(obj.ToString());
    }

    #endregion

    public static void RemoveReplayData(Action<bool> callback = null)
    {
        PlayFabAPI.SaveJSON(string.Empty, removeCallback =>
        {
            callback?.Invoke(removeCallback);
        });
    }

    public static void SaveReplayData(ReplayData data, Action<bool> callback = null)
    {
        var jsonData = JsonUtility.ToJson(data);

        PlayFabAPI.SaveJSON(jsonData, playFabCallback =>
        {
            callback?.Invoke(playFabCallback);
        });
    }

    public static void LoadReplayData(Action<bool, ReplayData> callback = null)
    {
        PlayFabAPI.LoadJSON((result, data) =>
        {
            if(data == string.Empty)
            {
                callback?.Invoke(false, null);
                return;
            }

            callback?.Invoke(result, JsonUtility.FromJson<ReplayData>(data));
        });
    }
}
