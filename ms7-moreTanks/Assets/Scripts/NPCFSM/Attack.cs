using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NPCBaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (target == null) return;
        targetTransform = target.transform;
        npcAI.StartFiring();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (target == null) return;
        npcAI.AimTurretToward(targetTransform.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        npcAI.StopFiring();
    }
}
