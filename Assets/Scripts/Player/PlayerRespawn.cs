using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRespawn : MonoBehaviour
{
    public float fallResetY = -10f;

    private Rigidbody rb;
    private Vector3 spawnPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        spawnPoint = transform.position;
    }

    void Update()
    {
        if (transform.position.y < fallResetY)
        {
            Respawn();
        }

        var keyboard = Keyboard.current;
        if (keyboard != null && keyboard.rKey.wasPressedThisFrame)
        {
            Respawn();
        }
    }

    public void SetCheckpoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = spawnPoint;
    }
}