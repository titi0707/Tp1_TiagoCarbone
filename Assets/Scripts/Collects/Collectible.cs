using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float bobHeight = 0.25f;
    public float bobSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoint();
            }
            Destroy(gameObject);
        }
    }
}