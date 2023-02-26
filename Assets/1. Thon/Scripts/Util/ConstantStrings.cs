using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public static class ConstantStrings
    {
        public static readonly string PREFAB_PATH = "Prefab/";

        static readonly string PREFAB = "Pf_";
        public static readonly string PREFAB_API = PREFAB + "API";

        public static readonly string PLAYFAB_EMAIL = "test@test.com";
        public static readonly string PLAYFAB_PASSWORD = "1234qwer!";

        public static readonly string ANI_CLIP_TOWER_IDLE = "Ani_Tower_Idle";
        public static readonly string ANI_CLIP_TOWER_ATK = "Ani_Tower_Atk";

        public static readonly string ANI_PARAM_TRIGER_TOWER_IDLE = "Idle";
        public static readonly string ANI_PARAM_TRIGER_TOWER_ATK = "Attack";
        public static readonly string ANI_PARAM_FLOAT_TOWER_ATTACK_SPEED = "AttackSpeed";
    }
}