using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour 
{
    protected GameObject npc;
    protected Transform npcTransform = null;
    protected TankAI npcAI;

    protected GameObject target;
    protected Transform targetTransform;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        npc = animator.gameObject;
        npcTransform = npc.transform;

        npcAI = npc.GetComponent<TankAI>();
        target = npcAI.GetPlayer();
    }
}
