using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour 
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.0f;

    public WaypointManager wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;



    private void Start() 
	{
        wps = wpManager.waypoints;
        graph = wpManager.CloneGraph();
        currentNode = GetNearestWaypoint();
    }


    private void LateUpdate() 
	{
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength()) {
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        //move to the next point if close enough
        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
                            transform.position) < accuracy) {
            currentWaypointIndex++;
        }

        //move 
        if (currentWaypointIndex < graph.getPathLength()) {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAt = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAt - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                            Quaternion.LookRotation(direction),
                                            Time.deltaTime * rotSpeed);
            transform.Translate(0,0,speed * Time.deltaTime);
        }
    }


    public void GoToWaypoint(int waypointIndex) {
        graph.AStar(currentNode, wps[waypointIndex]);
        currentWaypointIndex = 0;
    }

    private GameObject GetNearestWaypoint() {
        GameObject nearestWaypoint = null;
        float distanceToNearest = 0;
        for (int i = 0; i < wps.Length; i++) {
            if (nearestWaypoint == null) {
                nearestWaypoint = wps[i];
                distanceToNearest = Vector3.Distance(transform.position, wps[i].transform.position);
            } else {
                if (distanceToNearest > Vector3.Distance(transform.position, wps[i].transform.position)) {
                    nearestWaypoint = wps[i];
                    distanceToNearest = Vector3.Distance(transform.position, wps[i].transform.position);
                }
            }
        }

        return nearestWaypoint;
    }

}
