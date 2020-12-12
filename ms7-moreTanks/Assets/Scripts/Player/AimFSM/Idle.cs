using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Center turret
 */

public class Idle : StateMachineBehaviour
{
    private AimAssist aimAssist;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        aimAssist = animator.GetComponent<AimAssist>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        aimAssist.turret.rotation = Quaternion.Slerp(aimAssist.turret.rotation,
                    Quaternion.LookRotation(aimAssist.transform.forward),
                    Time.deltaTime * aimAssist.turretRotateSpeed);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
