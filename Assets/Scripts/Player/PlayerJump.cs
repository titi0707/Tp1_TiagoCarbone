using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 7f;

    private bool isGrounded = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (isGrounded && keyboard.spaceKey.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}