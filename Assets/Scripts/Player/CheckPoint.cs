using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
        if (playerRespawn != null)
        {
            playerRespawn.SetCheckpoint(transform.position);
            activated = true;

            
            Renderer rend = GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = Color.green;
            }
        }
    }
}