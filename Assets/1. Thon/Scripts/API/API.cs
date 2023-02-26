using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class API : Unit
    {
        static API _api;
        public static API Inst => _api;

        /// <summary>
        /// AfterAssembliesLoaded 의 경우 다른 스크립트에서도 사용되면 (포톤 처럼) 초기화 문제가 발생할 수 있으므로 BeforeSceneLoad 사용.
        /// - 설명 감사합미다. 용한넴
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RuntimeInit()
        {
            var go = Instantiate(Resources.Load<GameObject>(ConstantStrings.PREFAB_API));
            go.name = nameof(API);
            DontDestroyOnLoad(go);
            _api = go.GetComponent<API>();
        }

        public abstract class Part : UnitPart
        {
            protected API Upper => UpperUnit as API;
        }

        [Header("Parts")]
        public Replay Replay;
        public PlayFab PlayFab;

        private void Awake()
        {
            AddPart(Replay);
            AddPart(PlayFab);
        }
    }
}

