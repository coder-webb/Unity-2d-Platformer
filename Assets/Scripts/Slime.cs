using System;
using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour, ITakeHit
{
    Rigidbody2D _rigidbody;
    [SerializeField] Transform _leftSensor;
    [SerializeField] Transform _rightSensor;
    [SerializeField] Sprite _deadSprite;

    int _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _direction = -1;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.velocity = new Vector2(_direction, _rigidbody.velocity.y);

        if (_direction < 0)
        {
            ScanSensor(_leftSensor);
        }
        else
        {
            ScanSensor(_rightSensor);
        }
    }

    public void TakeDamage()
    {
        StartCoroutine(Die());
    }

    void ScanSensor(Transform sensor)
    {
        Debug.DrawRay(sensor.position, Vector2.down * 0.1f, Color.red);
        Debug.DrawRay(sensor.position, new Vector2(_direction, 0f) * 0.1f, Color.red);
        var raycastResultDown = Physics2D.Raycast(sensor.position, Vector2.down, 0.1f);
        var raycastResultSide = Physics2D.Raycast(sensor.position, new Vector2(_direction, 0f), 0.1f);

        if (raycastResultDown.collider == null || raycastResultSide.collider != null)
        {
            TurnAround();
        }
    }

    void TurnAround()
    {
        _direction *= -1;
        GetComponent<SpriteRenderer>().flipX = _direction > 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();
        var contacts = collision.contacts; // Pulls a ContactPoint2D array of contact points
        var normal = contacts[0].normal; // Pulls the first vector in the contacts array

        Debug.Log($"Normal = {normal}");

        if (player == null)
            return;
        else if (normal.y < -0.6)
            TakeDamage();
        else
            player.ResetToStart();



    }

    IEnumerator Die()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _deadSprite;

        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        enabled = false; // Means this.enabled = false, turning off this script
        GetComponent<Rigidbody2D>().simulated = false;

        float alpha = 1f;

        while (alpha > 0)
        {
            yield return null;
            alpha -= Time.deltaTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
        }

    }
}
