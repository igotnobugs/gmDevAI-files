using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{

    public GameObject targetToFollow;

    private Vector3 startingPos;
    private Transform tarTransform;
    private Vector3 deltaPosTowardsTarget;

    private void Start() 
	{
        tarTransform = targetToFollow.transform;
        deltaPosTowardsTarget = tarTransform.position - transform.position;
    }


    private void Update() 
	{
        Vector3 newPos = new Vector3(tarTransform.position.x, 
                                    transform.position.y,
                                    tarTransform.position.z);

        transform.position = tarTransform.position - deltaPosTowardsTarget;
    }
}
