using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
        public Vector3 offset = new Vector3(5f, 0f, 0f);
    public float speed = 2f;

    public Vector3 DeltaMovement { get; private set; }

    private Rigidbody rb;
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 targetPoint;
    private Vector3 previousPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        pointA = rb.position;
        pointB = rb.position + offset;
        targetPoint = pointB;
        previousPosition = rb.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPoint, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        DeltaMovement = newPosition - previousPosition;
        previousPosition = newPosition;

        if (Vector3.Distance(rb.position, targetPoint) < 0.05f)
        {
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 start = Application.isPlaying ? pointA : transform.position;
        Vector3 end = Application.isPlaying ? pointB : transform.position + offset;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(start, 0.3f);
        Gizmos.DrawWireSphere(end, 0.3f);
    }
}