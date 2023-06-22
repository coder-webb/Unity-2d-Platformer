using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ResetToStart();
        }
    }
}