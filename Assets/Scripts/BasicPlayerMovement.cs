using Cinemachine.PostFX;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BasicPlayerMovement : MonoBehaviour
{
    CharacterController cc;
    PlayerInput playerInput;

    public float moveSpeed;
    public float walkAcc = 5f;
    public float jumpHeight;

    public LayerMask groundLayers;
    public Transform groundCheck;
    public float groundCheckRadius;

    Vector3 v;
    bool isGrounded;

    // dash
    [SerializeField] DashManager dashManager;
    Collider dashHb;
    bool isDashing;
    float dashSpeed = 75f;
    Vector3 dashDir;
    float maxDashTime = 0.2f;
    float currDashDuration = 0f;
    float goodDashImpulseMag = 10f;

    // double jump
    bool doubleJumpAvailable;
    float doubleJumpTimerMax = 0.1f;
    float doubleJumpTimer;

    // sliding
    [SerializeField] Collider col;
    [SerializeField] PhysicMaterial defaultPMat;
    [SerializeField] PhysicMaterial slidePMat;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        dashManager.OnGoodDash.AddListener(() => EndDash(true));
        dashHb = dashManager.GetComponent<Collider>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isDashing)
        {
            MoveInDash();
            CheckDashDuration();
            return;
        }
        CheckGrounded();
        MoveCharacter(playerInput.moveInp, playerInput.jumpInp);
        AbilityCheck();
        ChangePhysicsMaterial();
    }

    void AbilityCheck()
    {
        if (playerInput.dashInp && !isDashing)
        {
            BeginDash();
        }
    }

    void BeginDash()
    {
        isDashing = true;
        dashHb.enabled = true;
        dashDir = Camera.main.transform.forward;
        currDashDuration = 0f;
    }

    void MoveInDash()
    {
        cc.Move(dashDir * dashSpeed * Time.deltaTime);
    }

    void CheckDashDuration()
    {
        currDashDuration += Time.deltaTime;
        if (currDashDuration > maxDashTime)
        {
            EndDash(false);
        }
    }

    void EndDash(bool hitValidTarget)
    {
        isDashing = false;
        dashHb.enabled = false;
        if (hitValidTarget)
        {
            v.y = goodDashImpulseMag;
            doubleJumpAvailable = true;
            doubleJumpTimer = doubleJumpTimerMax;
        } else
        {
            v.y = 0f;
        }
    }

    void ChangePhysicsMaterial()
    {
        col.material = Input.GetKey(KeyCode.C) ? slidePMat : defaultPMat;
    }


    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
    }
    private void MoveCharacter(Vector3 moveInp, bool jumpInp) {
        
        // clamp grounded velocity
        if (isGrounded && v.y < 0f)
        {
            v.y = -2f;
        }

        // input influence
        Vector3 moveDir = transform.right * moveInp.x + transform.forward * moveInp.z;
        cc.Move(moveDir * moveSpeed * Time.deltaTime);

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
        }
        else // in air
        {
            if (doubleJumpAvailable)
            {
                doubleJumpTimer -= Time.deltaTime;
                if (jumpInp && doubleJumpTimer <= 0)
                {
                    Debug.Log("double jump???");
                    DoJump();
                    doubleJumpAvailable = false;
                }
            }
        }

        // physics influence
        v.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(v * Time.deltaTime);
    }

    void DoJump()
    {
        v.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
    }
}
