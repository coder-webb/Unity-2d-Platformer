using UnityEngine;
using UnityEngine.Events;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] Sprite _pressedSprite;
    [SerializeField] UnityEvent _onPressed;
    [SerializeField] UnityEvent _onReleased;
    [SerializeField] int _playerNumber = 1;
    [SerializeField] bool spriteReset;

    SpriteRenderer _spriteRenderer;
    Sprite _releasedSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();

        if (player == null || player.PlayerNumber != _playerNumber)
            return;

        ButtonPressed();
    }

    void ButtonPressed()
    {
        _spriteRenderer.sprite = _pressedSprite;

        _onPressed?.Invoke();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();

        if (player == null || player.PlayerNumber != _playerNumber)
            return;

        ButtonRelease();
    }

    private void ButtonRelease()
    {
        if (spriteReset)
            _spriteRenderer.sprite = _releasedSprite;

        _onReleased?.Invoke();
    }
}
