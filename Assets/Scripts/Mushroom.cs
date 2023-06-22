using UnityEngine;

public class Mushroom : MonoBehaviour
{
     [SerializeField] float _bounceVelocity = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();
        var playerRB = player.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            if (playerRB != null)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, _bounceVelocity);
            }
        }
    }
}
