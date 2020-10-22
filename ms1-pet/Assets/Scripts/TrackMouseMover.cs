using UnityEngine;

public class TrackMouseMover : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 10.0f;

    private Vector3 targetVector = new Vector3();

    void LateUpdate() {
        //Mouse look
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
            targetVector = hit.point;
        }

        Vector3 targetDirection = targetVector - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, 
            targetDirection, Time.deltaTime * rotateSpeed, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection, new Vector3(0, 1, 0));


        //Movement
        float speedDt = moveSpeed * Time.deltaTime;
        transform.position += transform.forward * Input.GetAxis("Vertical") * speedDt;
        transform.position += transform.right * Input.GetAxis("Horizontal") * speedDt;
    }
}
