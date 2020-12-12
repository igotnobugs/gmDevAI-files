using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Aim towards closest
 */

public class Aim : StateMachineBehaviour
{
    private AimAssist aimAssist;
    public float searchRadius;

    private Tank closestTank;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        aimAssist = animator.GetComponent<AimAssist>();       
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Tank closestTarget = aimAssist.closestTarget;
        if (closestTarget == null) return;

        aimAssist.AimTurretToward(closestTarget.transform.position);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
