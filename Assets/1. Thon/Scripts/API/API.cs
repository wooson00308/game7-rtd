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
        /// AfterAssembliesLoaded �� ��� �ٸ� ��ũ��Ʈ������ ���Ǹ� (���� ó��) �ʱ�ȭ ������ �߻��� �� �����Ƿ� BeforeSceneLoad ���.
        /// - ���� �����չ̴�. ���ѳ�
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

