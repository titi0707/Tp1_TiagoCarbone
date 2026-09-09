using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                            // arrastrá acá tu Player
    public Vector3 offset = new Vector3(0f, 3.5f, -6f);  // atrás y arriba del personaje
    public float smoothTime = 0.15f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }
}