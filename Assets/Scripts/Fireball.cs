using UnityEngine;

public class Fireball : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField] float _fireballSpeed = 5;
    [SerializeField] float _fireballBounce = 5;

    public float Direction { get; set; }

    int _bouncesRemaining = 3;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_fireballSpeed * Direction, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ITakeHit hitable = collision.collider.GetComponent<ITakeHit>();

        if (hitable != null)
        {
            hitable.TakeDamage();
            Destroy(gameObject);
        }

        _bouncesRemaining--;
        if (_bouncesRemaining < 0)
            Destroy(gameObject);
        else
            _rigidbody.velocity = new Vector2(_fireballSpeed * Direction, _fireballBounce);
    }
}
