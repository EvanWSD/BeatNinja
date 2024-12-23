using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    PlayerInput playerInput;
    BeatManager beat;

    public float moveSpeed;
    public float walkAcc = 5f;
    public float jumpHeight;

    public LayerMask groundLayers;
    public Transform groundCheck;
    public float groundCheckRadius;

    // boolean states
    bool isGrounded;
    public bool isDashing { get; private set; }
    bool isSliding;

    // dash
    [SerializeField] DashManager dashManager;
    bool canDash;
    Collider dashHb;
    float dashSpeed = 75f;
    float recentDashTimerMax = 0.1f;
    float recentDashTimer = 0f;
    Vector3 dashDir;
    float maxDashTime = 0.2f;
    float currDashDuration = 0f;
    float goodDashImpulseMag = 10f;
    float startFov;
    [SerializeField] float maxDashFovDelta;

    // double jump
    bool doubleJumpAvailable;
    float doubleJumpTimerMax = 0.1f;
    float doubleJumpTimer;

    // sliding
    [SerializeField] Collider col;
    [SerializeField] PhysicMaterial defaultPMat;
    [SerializeField] PhysicMaterial slidePMat;
    Vector3 currSlideVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        dashManager.OnGoodDash.AddListener(() => EndDash(true));
        dashHb = dashManager.GetComponent<Collider>();
        col = GetComponent<Collider>();
        beat = GameObject.FindGameObjectWithTag("BeatManager").GetComponent<BeatManager>();

        startFov = Camera.main.fieldOfView;
    }

    private void Update()
    {
        CheckGrounded();
        recentDashTimer -= Time.deltaTime;
        if (isDashing)
        {
            MoveInDash();
            DashFov();
            CheckDashDuration();
            return;
        }
        MoveCharacter(playerInput.moveInp, playerInput.jumpInp);
        AbilityCheck();
        ChangePhysicsMaterial();
    }

    void ChangePhysicsMaterial()
    {
        col.material = Input.GetKey(KeyCode.C) ? slidePMat : defaultPMat;
    }

    void AbilityCheck()
    {
        if (canDash && playerInput.dashInp && !isDashing && beat.IsCalledNearBeat())
        {
            BeginDash();
        }
    }

    void BeginDash()
    {
        isDashing = true;
        canDash = false;
        dashHb.enabled = true;
        dashDir = Camera.main.transform.forward;
        currDashDuration = 0f;
    }

    void MoveInDash()
    {
        //cc.Move(dashDir * dashSpeed * Time.deltaTime);
        rb.velocity = dashDir * dashSpeed;
    }

    void CheckDashDuration()
    {
        currDashDuration += Time.deltaTime;
        if (currDashDuration > maxDashTime)
        {
            EndDash(false);
        }
    }

    void DashFov()
    {
        float dashProgress01 = currDashDuration / maxDashTime;
        float dashProgressSmoothed = Sigmoid((dashProgress01-0.5f)*4);
        Camera.main.fieldOfView = Mathf.Lerp(startFov, startFov+maxDashFovDelta, dashProgressSmoothed);
    }

    void EndDash(bool hitValidTarget)
    {
        isDashing = false;
        dashHb.enabled = false;
        recentDashTimer = recentDashTimerMax;
        Camera.main.fieldOfView = startFov;
        if (hitValidTarget)
        {
            rb.velocity = new Vector3(rb.velocity.x, goodDashImpulseMag, rb.velocity.z); 
            doubleJumpAvailable = true;
            doubleJumpTimer = doubleJumpTimerMax;
            canDash = true;
        }
        else
        {
            if (isGrounded && Input.GetKey(KeyCode.C))
            {
                isSliding = true;
                currSlideVelocity = dashDir * dashSpeed / 4f;
                currSlideVelocity.y = 0f;
                rb.velocity = currSlideVelocity;
            }
            //v.y = 0;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }
    private void MoveCharacter(Vector3 moveInp, bool jumpInp) {

        // input
        Vector3 moveDir = transform.right * moveInp.x + transform.forward * moveInp.z;
        if (!isSliding)
        {
            moveDir *= moveSpeed;
            rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);
        };

        // jump
        if (isGrounded && jumpInp)
        {
            DoJump();
            doubleJumpTimer = doubleJumpTimerMax;
        }

        // double jump + delay
        if (isGrounded)
        {
            doubleJumpAvailable = true;
            canDash = true;
        }
        else // in air
        {
            if (doubleJumpAvailable)
            {
                doubleJumpTimer -= Time.deltaTime;
                if (jumpInp && doubleJumpTimer <= 0)
                {
                    DoJump();
                    doubleJumpAvailable = false;
                }
            }
        }

        if (isSliding && Input.GetKeyUp(KeyCode.C))
        {
            isSliding = false;
            rb.velocity -= currSlideVelocity;
        }
    }

    void DoJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
    }

    public bool RecentlyDashed()
    {
        return recentDashTimer >= 0f;
    }

    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }
}
