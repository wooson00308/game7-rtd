using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class AuthPart : PlayFab.Part
    {
        public static Action<Result> OnLoginEvent;
        public static Action<Result> OnLogoutEvent;

        public abstract class NetworkState : UnitState
        {
            public AuthPart Upper => UpperUnit as AuthPart;
        }

        [Header("State")]
        public OnlineState OnlineState;
        public OfflineState OfflineState;

        protected void Awake()
        {
            AddState(OnlineState);
            AddState(OfflineState);

            SetStateOrNull(OfflineState);
        }

        public void OnLoginWithEmail(LoginWithEmailModel model, Action<Result> callback = null) 
        {
            PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest
            {
                TitleId = PlayFabSettings.TitleId,
                Email = model.email,
                Password = model.password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetPlayerProfile = true
                }
            },
            result =>
            {
                if(string.IsNullOrEmpty(result.InfoResultPayload.PlayerProfile.DisplayName))
                {
                    UserDataManager.AuthData.displayName = result.InfoResultPayload.PlayerProfile.DisplayName;
                }

                UserDataManager.AuthData.email = model.email;
                UserDataManager.AuthData.playFabId = result.PlayFabId;

                // TODO : SetStateOrNull(OnlineState);
                callback?.Invoke(new Result
                {
                    isSuccess = true,
                    message = "Login Success!"
                });
            },
            error =>
            {
                callback?.Invoke(new Result
                {
                    isSuccess = false,
                    message = "Login Failed!"
                });
            });
        }
    }
}