using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour 
{
    public GameObject obstacle;
    public GameObject newGoal;

    private GameObject[] agents;

    private void Start() {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }


    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) {
                Instantiate(obstacle, hit.point, obstacle.transform.rotation);

                for (int i = 0; i < agents.Length; i++) {
                    agents[i].GetComponent<AIControl>().DetectNewObstacle(hit.point);
                }

            }
        }

        
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) {
                GameObject nGoal = Instantiate(newGoal, hit.point, newGoal.transform.rotation);

                for (int i = 0; i < agents.Length; i++) {
                    agents[i].GetComponent<AIControl>().AddNewGoalLocation(nGoal);
                }
            }
        }
        
    }
}
