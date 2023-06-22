using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected;

    [SerializeField] List<AudioClip> _clips;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (player == null)
            return;

        CoinsCollected++;
        Debug.Log(CoinsCollected + " Coins collected");
        ScoringSystem.AddToScore(100);

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        AudioOnCoinPickup();
    }

    private void AudioOnCoinPickup()
    {
        if (_clips != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, _clips.Count);
            AudioClip randomClip = _clips[randomIndex];
            GetComponent<AudioSource>().PlayOneShot(randomClip);
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
