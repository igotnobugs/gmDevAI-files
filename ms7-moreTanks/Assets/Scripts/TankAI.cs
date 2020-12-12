using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : Tank 
{
    private Animator anim = null;
    private GameObject player = null;
    private Transform playerTransform = null;

    [Header("AI Pathing")]
    public float wayPointAccuracy = 3.0f;
    public GameObject[] wayPoints;
    public int currentWaypoint;
    public Transform curWayTransform = null;
    
    private void Start() {
        if (anim == null) {
            anim = GetComponent<Animator>();
        }

        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");          
        }

        playerTransform = player.transform;

        wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
        currentWaypoint = 0;
    }

    private void Update() {
        if (player == null) {
            anim.SetFloat("distance", 9999999);
            return;
        }
        anim.SetFloat("distance", 
            Vector3.Distance(transform.position, playerTransform.position));

        anim.SetFloat("hpPercent", GetHealthPerc());
    }

    public GameObject GetPlayer() {
        return player;
    }
}
