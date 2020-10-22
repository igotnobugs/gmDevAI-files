using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AIControl : MonoBehaviour 
{
    public enum AIBehaviour {Pursue, Coward, Evader };

    public GameObject target;
    public float sightRange;
    public AIBehaviour behaviour;

    private NavMeshAgent agent;
    private Transform tarTransform;
    private WASDMovement playerMove;
    private Vector3 targetingPosition;

    private void Start() 
	{
        agent = GetComponent<NavMeshAgent>();
        playerMove = target.GetComponent<WASDMovement>();

        tarTransform = target.transform;  
    }

    private void Update() 
	{
        float distToTarget = Vector3.Distance(tarTransform.position, transform.position);

        if (CanSeeTarget() && distToTarget <= sightRange) {
            switch (behaviour) {
                case AIBehaviour.Pursue:
                    Pursue();
                    break;
                case AIBehaviour.Coward:
                    CleverHide();
                    break;
                case AIBehaviour.Evader:
                    Evade();
                    break;
            }
        } else {
            Wander();
        }


    }

    private void Seek(Vector3 toLocation) {
        agent.SetDestination(toLocation);
    }

    private void Flee(Vector3 fromLocation) {
        Vector3 fleeDir = fromLocation - transform.position;
        agent.SetDestination(transform.position - fleeDir);
    }

    private void Pursue() {
        targetingPosition = PredictDirection(tarTransform);
        Seek(targetingPosition);
    }

    private void Evade() {
        targetingPosition = PredictDirection(tarTransform);
        Flee(targetingPosition);
    }

    private void Wander() {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;
        Vector3 wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                           0,
                                           Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        targetingPosition = transform.InverseTransformVector(targetLocal);
        Seek(targetingPosition);
    }

    private void Hide() {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        int distanceOffset = 5;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++) {
            Vector3 hideSpotPos = World.Instance.GetHidingSpots()[i].transform.position;

            Vector3 hideDir = hideSpotPos - tarTransform.position;
            Vector3 hidePos = hideSpotPos + hideDir.normalized * distanceOffset;

            float spotDist = Vector3.Distance(transform.position, hidePos);
            if (spotDist < distance) {
                chosenSpot = hidePos;
                distance = spotDist;
            }
        }

        targetingPosition = chosenSpot;
        Seek(targetingPosition);
    }

    private void CleverHide() {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;        
        GameObject chosenObject = World.Instance.GetHidingSpots()[0];

        int distanceOffset = 5;
        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++) {
            GameObject hideObject = World.Instance.GetHidingSpots()[i];
            Vector3 hideSpotPos = hideObject.transform.position;

            Vector3 hideDir = hideSpotPos - tarTransform.position;
            Vector3 hidePos = hideSpotPos + hideDir.normalized * distanceOffset;

            float spotDist = Vector3.Distance(transform.position, hidePos);
            if (spotDist < distance) {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenObject = hideObject;
                distance = spotDist;
            }
        }

        Collider hideCol = chosenObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float rayDistance = Mathf.Max(distanceOffset + 1.0f, 100.0f);
        hideCol.Raycast(back, out info, rayDistance);

        
        targetingPosition = info.point + chosenDir.normalized * distanceOffset;
        Seek(targetingPosition);
    }

    private Vector3 PredictDirection(Transform target) {
        Vector3 tarDir = target.position - transform.position;
        float lookAhead = tarDir.magnitude / (agent.speed + playerMove.currentSpeed);
        Vector3 targetPosition = tarDir + tarTransform.position * lookAhead;

        return targetPosition;
    }

    private bool CanSeeTarget() {
        Vector3 rayToTarget = tarTransform.position - transform.position;

        if (Physics.Raycast(transform.position, rayToTarget, out RaycastHit info)) {
            return info.transform.gameObject.tag == "Player";
        }

        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(targetingPosition, 0.2f);

        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
