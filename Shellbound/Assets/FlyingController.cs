using UnityEngine;

public class FlyingController : MonoBehaviour
{
    Rigidbody rb;
    float speed = 8;
    float fastSpeed = 24;

    float horizontalInput;
    float verticalInput;

    public Transform orientation;
    Vector3 flatMoveDirection;
    Vector3 aerialMoveDirection;

    bool sprinting;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();

    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        flatMoveDirection = new(horizontalInput, verticalInput);

        if (Input.GetKey(KeyCode.Space))
        {
            aerialMoveDirection = new(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            aerialMoveDirection = new(0, -1, 0);
        }
        else
        {
            aerialMoveDirection = Vector3.zero;
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
    }

    void MovePlayer()
    {
        float xMovement;
        float yMovement;
        float zMovement;

        float currentSpeed = speed;
        if (sprinting)
        {
            currentSpeed = fastSpeed;
        }

        flatMoveDirection = (orientation.forward * verticalInput) + orientation.right * horizontalInput;
        flatMoveDirection.Normalize();

        xMovement = flatMoveDirection.x * currentSpeed;
        yMovement = aerialMoveDirection.y * currentSpeed;
        zMovement = flatMoveDirection.z * currentSpeed;

        rb.velocity = new(xMovement, yMovement, zMovement);
    }
}
