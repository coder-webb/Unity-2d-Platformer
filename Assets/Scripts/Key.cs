using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyLock _keyLock;
    [SerializeField] string _lockColor;
    [SerializeField] int _keyDurability;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
        }

        var keyLock = collision.GetComponent<KeyLock>();
        if (keyLock != null && keyLock._lockColor == _lockColor)
        {
            keyLock.Unlock();
            _keyDurability--;

            if (_keyDurability == 0)
                Destroy(gameObject);
        }
    }
}
