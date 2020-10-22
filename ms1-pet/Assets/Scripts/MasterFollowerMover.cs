using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterFollowerMover : MonoBehaviour {

    public float moveSpeed = 4.0f;
    public float rotationSpeed = 3.0f;

    public GameObject objectToFollow;
    public float nearRadius = 2.0f;

    private Vector3 targetDirection;


    void LateUpdate() {
        Vector3 targetPosition = new Vector3(objectToFollow.transform.position.x,
            transform.position.y,
            objectToFollow.transform.position.z);

        targetDirection = targetPosition - transform.position;

        //Look Rotation
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(targetDirection),
            Time.deltaTime * rotationSpeed);

        if (targetDirection.magnitude > nearRadius) {

            //Just moving forward
            //transform.Translate(0, 0, Time.deltaTime * moveSpeed);

            //Moving with Lerp
            transform.position = Vector3.Lerp(transform.position,
                targetPosition,
                Time.deltaTime * (moveSpeed / 4.0f));
        }
    }
}
