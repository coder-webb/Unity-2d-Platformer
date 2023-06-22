using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] float _bounceVelocity = 10;

    [SerializeField] Sprite _downSprite;
    
    Sprite _upSprite;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();

        if (player == null)
            return;

        var playerRB = player.GetComponent<Rigidbody2D>();

        if (player != null)
        {
            if (playerRB != null)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, _bounceVelocity);
                _spriteRenderer.sprite = _downSprite;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();

        if (player != null)
        {
            _spriteRenderer.sprite = _upSprite;
        }
    }
}
