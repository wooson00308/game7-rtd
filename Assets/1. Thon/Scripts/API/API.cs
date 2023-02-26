using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze.API
{
    public class API : MUnit<API>
    {
        /// <summary>
        /// AfterAssembliesLoaded �� ��� �ٸ� ��ũ��Ʈ������ ���Ǹ� (���� ó��) �ʱ�ȭ ������ �߻��� �� �����Ƿ� BeforeSceneLoad ���.
        /// - ���� �����չ̴�. ���ѳ�
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RuntimeInit()
        {
            var go = Instantiate(Resources.Load<GameObject>($"{ConstantStrings.PREFAB_PATH}{ConstantStrings.PREFAB_API}"));
            go.name = nameof(API);
            DontDestroyOnLoad(go);
        }

        public abstract class Part : UnitPart
        {
            protected API Upper => UpperUnit as API;
        }

        [Header("Parts")]
        public Replay Replay;
        public PlayFab PlayFab;

        protected override void Awake()
        {
            base.Awake();

            AddPart(Replay);
            AddPart(PlayFab);
        }
    }
}

