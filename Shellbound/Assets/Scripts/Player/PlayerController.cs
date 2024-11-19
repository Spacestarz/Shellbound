using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    bool dashing;
    bool readyToDash;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

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
        LimitToMaxSpeed();
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
                    Invoke(nameof(ResetDashCooldown), dashCooldown);
                }
            }
        }
        
        if (!PlayerSlice.SliceMode() && Input.GetButtonDown("Fire1"))
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

        if (Input.GetButtonDown("Fire2"))
        {
            slice.ToggleSliceMode();
            
            horizontalInput = 0;
            verticalInput = 0;
        }

        if (PlayerSlice.SliceMode() && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Slice begun!");
            slice.ToggleIsSlicing();
        }

        if (PlayerSlice.SliceMode() && Input.GetButton("Fire1"))
        {
            slice.SliceRayCast();
        }

        if (PlayerSlice.SliceMode() && Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Slice Over");
            slice.ToggleIsSlicing();
        }
    }

    private void HandleDrag()
    {
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;
    }

    private void MovePlayer()
    {
        if (!dashing)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        }

        if (grounded && !dashing)
        {
            rb.AddForce(moveDirection.normalized * maxSpeed * 10f, ForceMode.Force);
        }

        else if (!grounded && !dashing)
        {
            rb.AddForce(moveDirection.normalized * maxSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        else if (dashing)
        {
            rb.velocity = moveDirection.normalized * dashForce;
        }
    }

    private void LimitToMaxSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (!dashing && flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
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

        rb.drag = 0;
    }

    private void EndDash()
    {
        dashing = false;

        rb.drag = groundDrag;
    }

    private void ResetDashCooldown()
    {
        readyToDash = true;
    }
}
