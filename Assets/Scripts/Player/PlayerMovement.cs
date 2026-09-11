using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private MovingPlatform currentPlatform;
    private Dash dash;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dash = GetComponent<Dash>();
    }

    void FixedUpdate()
    {
        
        if (dash != null && dash.IsDashing)
        {
            rb.linearVelocity = new Vector3(dash.DashDirection.x * dash.dashSpeed, rb.linearVelocity.y, dash.DashDirection.z * dash.dashSpeed);
            return;
        }

        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float h = 0f, v = 0f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) h = -1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) h = 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) v = -1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) v = 1f;

        Vector3 inputDir = new Vector3(h, 0f, v);
        Vector3 moveVelocity = inputDir.normalized * moveSpeed;

        Vector3 platformVelocity = Vector3.zero;
        if (currentPlatform != null)
        {
            platformVelocity = currentPlatform.DeltaMovement / Time.fixedDeltaTime;
        }

        rb.linearVelocity = new Vector3(moveVelocity.x + platformVelocity.x, rb.linearVelocity.y, moveVelocity.z + platformVelocity.z);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir.normalized, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }

    void OnCollisionStay(Collision collision)
    {
        MovingPlatform platform = collision.collider.GetComponent<MovingPlatform>();
        if (platform == null) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                currentPlatform = platform;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<MovingPlatform>() == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}