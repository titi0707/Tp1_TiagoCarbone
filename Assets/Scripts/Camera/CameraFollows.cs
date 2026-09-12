using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 7f, -9f);
    public float smoothTime = 0.2f;
    public float collisionBuffer = 0.3f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 direction = desiredPosition - target.position;
        float distance = direction.magnitude;

        
        RaycastHit hit;
        if (Physics.Raycast(target.position, direction.normalized, out hit, distance))
        {
            desiredPosition = target.position + direction.normalized * (hit.distance - collisionBuffer);
        }

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target.position + Vector3.up * 0.3f);
    }
}