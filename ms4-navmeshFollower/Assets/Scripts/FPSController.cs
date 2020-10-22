using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour 
{
    [SerializeField] private Transform cam = null;
    [SerializeField] private float mouseSensitivity = 70.0f;
    [SerializeField] private float headRotationLimit = 80.0f;

    public float moveSpeed = 10.0f;
    public float jumpForce = 5.0f;

    private Rigidbody rb;
    private float headRotation = 0f;
    private CapsuleCollider capCollider;

    private void Start() 
	{
        rb = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update() 
	{
        //Mouse
        float mouseSenseDt = mouseSensitivity * Time.deltaTime;
        float xMouse = Input.GetAxis("Mouse X") * mouseSenseDt;
        float yMouse = Input.GetAxis("Mouse Y") * mouseSenseDt * -1.0f;
        transform.Rotate(0.0f, xMouse, 0.0f);
        headRotation += yMouse;
        headRotation = Mathf.Clamp(headRotation, -headRotationLimit, headRotationLimit);
        cam.localEulerAngles = new Vector3(headRotation, 0.0f, 0.0f);

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //Keyboard Movement     
        float speedDt = moveSpeed * Time.deltaTime;
        float xKey = Input.GetAxisRaw("Horizontal");
        float zKey = Input.GetAxisRaw("Vertical");
        Vector3 moveVector = transform.right * xKey + transform.forward * zKey;
        rb.MovePosition(transform.position + moveVector.normalized * speedDt);
    }

    private bool IsGrounded() {
        float extraHeight = 0.1f;
        return Physics.Raycast(capCollider.bounds.center, Vector3.down, capCollider.bounds.extents.y + extraHeight);
    }
}
