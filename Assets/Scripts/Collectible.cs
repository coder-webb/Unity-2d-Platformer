using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action OnPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player == null)
            return;

        gameObject.SetActive(false);

        // event has to be checked if it's not null. Use ? or an if statement if (OnpPickedUp != null) 
        OnPickedUp?.Invoke();
    }
}