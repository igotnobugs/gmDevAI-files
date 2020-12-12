using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCBaseFSM
{
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (target == null) return;
        targetTransform = target.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (target == null) return;
        npcAI.MoveTowards(targetTransform.position);
        npcAI.AimTurretToward(targetTransform.position);
    }
}
