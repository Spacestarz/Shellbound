using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump;

    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    bool dashing;
    bool readyToDash;

    bool knockedBack;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [HideInInspector] public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 flatVelo;

    Rigidbody rb;
    PlayerSlice slice;
    Fire fire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        slice = GetComponent<PlayerSlice>();
        fire = GetComponent<Fire>();

        dashing = false;

        readyToJump = true;
        readyToDash = true;
    }

    private void Update()
    {
        GroundCheck();
        GetInputs();
        HandleDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void GetInputs()
    {
        if (!dashing)
        {
            if (!PlayerSlice.SliceMode())
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");

                //Space to jump
                if (Input.GetButton("Jump") && readyToJump && grounded)
                {
                    readyToJump = false;
                    Jump();
                    Invoke(nameof(ResetJumpCooldown), jumpCooldown);
                }
                //Shift to dash
                if (Input.GetButton("Fire3") && readyToDash && grounded && moveDirection != Vector3.zero)
                {
                    readyToDash = false;
                    Dash();
                    Invoke(nameof(EndDash), dashDuration);
                }
            }
        }

        if (Input.GetButtonDown("Fire1") && !PlayerSlice.SliceMode())
        {
            if (fire.fired)
            {
                fire.ReturnHarpoon();
            }
            else
            {
                fire.FireHarpoon();
            }
        }
    }

    private void HandleDrag()
    {
        if (grounded && !dashing && !knockedBack)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void MovePlayer()
    {
        float xMovement;
        float zMovement;
        if (!dashing)
        {
            moveDirection = (orientation.forward * verticalInput) + orientation.right * horizontalInput;
            moveDirection.Normalize();
        }
            xMovement = moveDirection.x * maxSpeed;
            zMovement = moveDirection.z * maxSpeed;

        if (grounded && !dashing && !knockedBack)
        {
            rb.velocity = new Vector3(xMovement, rb.velocity.y, zMovement);
        }
        else if (!grounded && !dashing && !knockedBack)
        {
            if (moveDirection != Vector3.zero)
            {
                rb.velocity = new Vector3(xMovement, rb.velocity.y, zMovement);
            }
        }
        else if (dashing)
        {
            rb.velocity = moveDirection.normalized * dashForce;
        }
    }


    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJumpCooldown()
    {
        readyToJump = true;
    }

    private void Dash()
    {
        dashing = true;
    }

    private void EndDash()
    {
        dashing = false;

        rb.drag = groundDrag;
        Invoke(nameof(ResetDashCooldown), dashCooldown);
    }

    private void ResetDashCooldown()
    {
        readyToDash = true;
    }

    public void NullifyMovement()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }

    public void GetKnockedBack()
    {
        knockedBack = true;
        rb.drag = 0;
        Invoke(nameof(ResetKnockedBack), 0.2f);
    }

    void ResetKnockedBack()
    {
        knockedBack = false;
        rb.drag = groundDrag;
    }
}
