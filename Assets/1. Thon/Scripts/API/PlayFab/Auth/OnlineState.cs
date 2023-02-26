using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    /// <summary>
    /// �¶��� ���� 
    /// 
    /// TODO
    /// 
    /// �¶��� ���¿����� �ֱ������� ������ ����Ͽ� ����Ǿ��ִ��� Ȯ�����־�� �Ѵ�.
    /// State�� Active �Ǿ��� �� endTime�� �����ϰ� lapse�� ������.
    /// endTime�� �Ǹ�, ������ ���.
    /// 
    /// faild ó�� �ȴٸ� �������� ���·� ��ȯ�Ѵ�.
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
                    // Ÿ�̸� �ٽ� ������
                    SetIsUseEndTime(3000);
                }
                else
                {
                    // TODO : ������ �������ٴ� �˸�
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
                // TODO : Ŭ���� ��ũ��Ʈ���� ��ȯ�Ǵ� �� �޾ƿ���

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