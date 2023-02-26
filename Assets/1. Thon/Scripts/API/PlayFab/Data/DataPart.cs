using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class DataPart : PlayFab.Part
    {
        public void RemoveReplayData(Action<bool> callback = null)
        {
            SaveJSON(string.Empty, removeCallback =>
            {
                callback?.Invoke(removeCallback);
            });
        }

        public void SaveReplayData(ReplayData data, Action<bool> callback = null)
        {
            var jsonData = JsonUtility.ToJson(data);

            SaveJSON(jsonData, playFabCallback =>
            {
                callback?.Invoke(playFabCallback);
            });
        }

        public void LoadReplayData(Action<bool, ReplayData> callback = null)
        {
            LoadJSON((result, data) =>
            {
                if (data == string.Empty)
                {
                    callback?.Invoke(false, null);
                    return;
                }

                callback?.Invoke(result, JsonUtility.FromJson<ReplayData>(data));
            });
        }

        void SaveJSON(string data, Action<bool> callback = null)
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

        void LoadJSON(Action<bool, string> callback = null)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = UserDataManager.AuthData.playFabId,
                Keys = new System.Collections.Generic.List<string> { "ReplayData" }
            },
            result =>
            {
                if (result.Data.Count == 0)
                {
                    callback?.Invoke(false, string.Empty);
                }

                else if (string.IsNullOrEmpty(result.Data["ReplayData"].Value))
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
}