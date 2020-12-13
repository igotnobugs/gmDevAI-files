using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour 
{
    [SerializeField] private GameObject[] goalLocations;
    private NavMeshAgent agent;
    private Animator anim;
    private float speedMultiplier;

    private float detectionRadius = 20;
    private float fleeRadius = 20;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        
        int randomInt = Random.Range(0, goalLocations.Length);
        Vector3 randomLocation = goalLocations[randomInt].transform.position;
        agent.SetDestination(randomLocation);

        float randomFloat = Random.Range(0.1f, 1.0f);
        anim.SetFloat("wOffset", randomFloat);

        ResetAgent();
    }


    private void LateUpdate() {
        if (agent.remainingDistance < 1) {
            ResetAgent();
            int randomInt = Random.Range(0, goalLocations.Length);
            Vector3 randomLocation = goalLocations[randomInt].transform.position;
            agent.SetDestination(randomLocation);
        }
    }

    public void ResetAgent() {
        speedMultiplier = Random.Range(0.1f, 1.5f);
        agent.speed = 2 * speedMultiplier;
        agent.angularSpeed = 120;
        anim.SetFloat("speedMultiplier", speedMultiplier);
        anim.SetTrigger("isWalking");
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 location) {
        if (Vector3.Distance(location, transform.position) < detectionRadius) {
            Vector3 fleeDirection = (transform.position - location).normalized;
            Vector3 newGoal = transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);
            if (path.status != NavMeshPathStatus.PathInvalid) {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    
    public void AddNewGoalLocation(GameObject go) {

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(go.transform.position, path);

        if (path.status != NavMeshPathStatus.PathInvalid) {
            GameObject[] newArray = new GameObject[goalLocations.Length + 1];
            for (int i = 0; i < goalLocations.Length; i++) {
                newArray[i] = goalLocations[i];
            }
            newArray[newArray.Length - 1] = go;
            goalLocations = newArray;
        }
    }
    
}
