using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        npcAI.curWayTransform = npcAI.wayPoints[npcAI.currentWaypoint].transform;    
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (npcAI.wayPoints.Length <= 0) return;

        if (Vector3.Distance(npcAI.curWayTransform.position,
            npcTransform.position) < npcAI.wayPointAccuracy) {
            npcAI.currentWaypoint++;
            if (npcAI.currentWaypoint >= npcAI.wayPoints.Length) {
                npcAI.currentWaypoint = 0;
            }
            npcAI.curWayTransform = npcAI.wayPoints[npcAI.currentWaypoint].transform;
        }

        npcAI.MoveTowards(npcAI.curWayTransform.position);
        npcAI.AimTurretToward(npcAI.curWayTransform.position);     
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
