using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[System.Serializable]
public struct Link {
    public enum Direction { UNI, BI };
    public GameObject node1;
    public GameObject node2;
    public Direction dir;
}

public class WaypointManager : MonoBehaviour 
{
    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();

    private void Start() 
	{
        graph = PopulateGraph();
    }

    private void Update() 
	{
        graph.debugDraw();
    }

    private Graph PopulateGraph() {
        Graph newGraph = new Graph();

        if (waypoints.Length > 0) {
            foreach (GameObject wp in waypoints) {
                newGraph.AddNode(wp);
            }
            foreach (Link l in links) {
                newGraph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.Direction.BI) {
                    newGraph.AddEdge(l.node2, l.node1);
                }
            }
        }

        return newGraph;
    }

    //Give an exact copy of the graph
    public Graph CloneGraph() {
        return PopulateGraph();
    }

}