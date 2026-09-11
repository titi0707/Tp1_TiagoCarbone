using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashSpeed = 18f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    public bool IsDashing { get; private set; }
    public Vector3 DashDirection { get; private set; }

    private float dashTimer;
    private float cooldownTimer;

    void Update()
    {
        if (cooldownTimer > 0f) cooldownTimer -= Time.deltaTime;

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.leftShiftKey.wasPressedThisFrame && !IsDashing && cooldownTimer <= 0f)
        {
            IsDashing = true;
            dashTimer = dashDuration;
            DashDirection = transform.forward;
        }

        if (IsDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                IsDashing = false;
                cooldownTimer = dashCooldown;
            }
        }
    }
}