using UnityEngine;

public class Breakable : MonoBehaviour, ITakeHit
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerController>() == null)
            return;

        if (collision.contacts[0].normal.y > 0)
            TakeDamage();
    }

    public void TakeDamage()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
