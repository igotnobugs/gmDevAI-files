using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankManager : MonoBehaviour 
{

    public GameObject[] tanks;

    private GameObject currentTank;
    private int currentTankIndex = 0;

    public Text tankName;

    private void Start() 
	{
        currentTank = tanks[0];
    }

    private void Update() 
	{
        if (currentTank == null) return;
        tankName.text = currentTank.name;
    }

    public void TankGoToWaypoint(int waypointIndex) {
        currentTank.GetComponent<FollowPath>().GoToWaypoint(waypointIndex);
    }

    public void CycleNext() {
        currentTankIndex++;
        if (currentTankIndex >= tanks.Length) {
            currentTankIndex = 0;
        }
        currentTank = tanks[currentTankIndex];
    }

    public void CyclePrevious() {
        currentTankIndex--;
        if (currentTankIndex < 0) {
            currentTankIndex = tanks.Length - 1;
        }
        currentTank = tanks[currentTankIndex];
    }
}
