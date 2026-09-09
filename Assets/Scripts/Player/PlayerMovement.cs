using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float h = 0f, v = 0f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) h = -1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) h = 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) v = -1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) v = 1f;

        Vector3 moveDir = new Vector3(h, 0f, v).normalized * moveSpeed;
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
    }
}