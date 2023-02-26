using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    /// <summary>
    /// 온라인 상태 
    /// 
    /// TODO
    /// 
    /// 온라인 상태에서는 주기적으로 서버와 통신하여 연결되어있는지 확인해주어야 한다.
    /// State가 Active 되었을 시 endTime을 설정하고 lapse를 돌린다.
    /// endTime이 되면, 서버에 통신.
    /// 
    /// faild 처리 된다면 오프라인 상태로 전환한다.
    /// </summary>
    public class OnlineState : AuthPart.NetworkState
    {
        public override void Activate()
        {
            base.Activate();
            SetIsUseEndTime(3000);
        }

        public override void Inactivate()
        {
            base.Inactivate();
        }

        protected override void OnEndTime()
        {
            base.OnEndTime();

            CheckPlayFabServer(result =>
            {
                Log(result.message);

                if(result.isSuccess)
                {
                    // 타이머 다시 돌리기
                    SetIsUseEndTime(3000);
                }
                else
                {
                    // TODO : 접속이 끊어졌다는 알림
                    Upper.SetStateOrNull(Upper.OfflineState);
                }
            });
        }

        void CheckPlayFabServer(Action<Result> callback = null)
        {
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest 
            { 
                FunctionName = "CheckPlayFabServer", 
                FunctionParameter = "Test Parameter",
            },
            result =>
            {
                // TODO : 클라우드 스크립트에서 반환되는 값 받아오기

                callback?.Invoke(new Result
                {
                    isSuccess = true,
                    message = "Server Connected Complete!"
                });
            },
            error =>
            {
                callback?.Invoke(new Result
                {
                    isSuccess = false,
                    message = "Server Connected Failed!"
                });
            });
        }
    }
}