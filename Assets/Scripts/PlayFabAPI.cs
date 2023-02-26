using PlayFab;
using System;
using PlayFab.ClientModels;

/// <summary>
/// old Scripts
/// </summary>
public static class PlayFabAPI
{
    static string PlayFabID;
    public static void Login(Action<bool> callback = null)
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest
        {
            TitleId = PlayFabSettings.TitleId,
            Email = "test@test.com",
            Password = "1234qwer!",
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        },
        result =>
        {
            PlayFabID = result.PlayFabId;
            callback?.Invoke(true);
        },
        error =>
        {
            callback?.Invoke(false);
            ReplayUtil.LogError(error.ErrorMessage);
        });
    }

    public static void SaveJSON(string data, Action<bool> callback = null)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                { "ReplayData", data }
            }
        },
        result =>
        {
            callback?.Invoke(true);
        },
        error =>
        {
            callback?.Invoke(false);
            ReplayUtil.LogError(error.ErrorMessage);
        });
    }

    public static void LoadJSON(Action<bool, string> callback = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = PlayFabID,
            Keys = new System.Collections.Generic.List<string> { "ReplayData" }
        },
        result =>
        {
            if(result.Data.Count == 0)
            {
                callback?.Invoke(false, string.Empty);
            }

            else if(string.IsNullOrEmpty(result.Data["ReplayData"].Value))
            {
                callback?.Invoke(false, string.Empty);
            }

            else
            {
                callback?.Invoke(true, result.Data["ReplayData"].Value);
            }
        },
        error =>
        {
            callback?.Invoke(false, string.Empty);
            ReplayUtil.LogError(error.ErrorMessage);
        });
    }
}
