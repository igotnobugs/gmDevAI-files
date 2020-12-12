using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NPCBaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (target == null) return;
        targetTransform = target.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (target == null) return;
        npcAI.MoveAway(targetTransform.position);   
        npcAI.AimTurretToward(npcAI.transform.position);
    }
}
