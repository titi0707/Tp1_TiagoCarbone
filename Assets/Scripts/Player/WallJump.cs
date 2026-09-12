using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class WallJump : MonoBehaviour
{
    public float wallJumpForce = 10f;
    public float wallJumpUpForce = 8f;
    public float wallSlideSpeed = 2f;
    public float controlLockDuration = 0.25f;

    public bool IsWallJumping { get; private set; }
    public Vector3 WallJumpVelocity { get; private set; }

    private Rigidbody rb;
    private PlayerJump playerJump;
    private bool touchingWall;
    private Vector3 wallNormal;
    private float lockTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        if (lockTimer > 0f)
        {
            lockTimer -= Time.deltaTime;
            if (lockTimer <= 0f) IsWallJumping = false;
        }

       
        if (touchingWall && !IsWallJumping && rb.linearVelocity.y < -wallSlideSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -wallSlideSpeed, rb.linearVelocity.z);
        }

        var keyboard = Keyboard.current;
        if (touchingWall && !IsWallJumping && keyboard != null && keyboard.spaceKey.wasPressedThisFrame)
        {
            Vector3 push = wallNormal * wallJumpForce;
            WallJumpVelocity = new Vector3(push.x, wallJumpUpForce, push.z);
            IsWallJumping = true;
            lockTimer = controlLockDuration;
            touchingWall = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        bool isGrounded = playerJump != null && playerJump.IsGrounded;
        if (isGrounded) return; 

        foreach (ContactPoint contact in collision.contacts)
        {
            if (Mathf.Abs(contact.normal.y) < 0.3f)
            {
                touchingWall = true;
                wallNormal = contact.normal;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        touchingWall = false;
    }
}