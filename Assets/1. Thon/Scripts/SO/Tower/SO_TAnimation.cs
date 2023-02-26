using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    /// <summary>
    /// Tower의 Animation 스크립터블 오브젝트
    /// </summary>
    [CreateAssetMenu(fileName = "SO_TAnimation", menuName = "SO/RTD/Tower/Animation", order = 0)]
    public class SO_TAnimation : ScriptableObject
    {
        [SerializeField] protected AnimationClip _idleClip;
        [SerializeField] protected AnimationClip _attackClip;

        //public void OverrideAnimator(Animator animator)
        //{
        //    var overrideController = new AnimatorOverrideController
        //    {
        //        runtimeAnimatorController = animator.runtimeAnimatorController
        //    };

        //    overrideController[ConstantStrings.ANI_CLIP_TOWER_IDLE] = _idleClip;
        //    overrideController[ConstantStrings.ANI_CLIP_TOWER_ATK] = _attackClip;
        //}
    }
}