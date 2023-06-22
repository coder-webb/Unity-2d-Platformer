using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _openMid;
    [SerializeField] Sprite _openTop;
    [SerializeField] SpriteRenderer _rendererMid;
    [SerializeField] SpriteRenderer _rendererTop;
    [SerializeField] Canvas _canvas;
    [SerializeField] Door _exit;

    bool _open;

    int _requiredCoins = 3;

    [ContextMenu("Open Door")]
    public void open()
    {
        _rendererMid.sprite = _openMid;
        _rendererTop.sprite = _openTop;
        _open = true;
        if (_canvas != null)
            _canvas.enabled = false;
    }

    void Update()
    {
        if (!_open && Coin.CoinsCollected >= _requiredCoins)
            open();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_open)
            return;
        var player = collision.GetComponent<PlayerController>();
        if (player != null && _exit != null)
            player.TeleportTo(_exit.transform.position);
    }
}
