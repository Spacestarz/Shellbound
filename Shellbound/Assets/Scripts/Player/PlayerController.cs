using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        GetInputs();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //calculate direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.velocity = moveDirection.normalized * moveSpeed;
    }
}
