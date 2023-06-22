using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] ToggleDirection _startingDirection = ToggleDirection.center;

    [SerializeField] Sprite _leftSprite;
    [SerializeField] Sprite _centerSprite;
    [SerializeField] Sprite _rightSprite;
    [SerializeField] UnityEvent _switchLeft;
    [SerializeField] UnityEvent _switchCenter;
    [SerializeField] UnityEvent _switchRight;

    SpriteRenderer _spriteRenderer;

    ToggleDirection _currentDirection;

    enum ToggleDirection
    {
        left,
        center,
        right
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ToggleSwitch(_startingDirection, true);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (!player)
            return;

        var playerRigidbody = collision.GetComponent<Rigidbody2D>();

        if (!playerRigidbody)
            return;

        bool playerMovingRight = playerRigidbody.velocity.x > 0;
        bool playerMovingLeft = playerRigidbody.velocity.x < 0;
        bool playerOnRightSide = player.transform.position.x > transform.position.x;

        if (playerMovingRight && playerOnRightSide)
            ToggleSwitch(ToggleDirection.right);
        else if (playerMovingLeft && !playerOnRightSide)
            ToggleSwitch(ToggleDirection.left);
    }

    void ToggleSwitch(ToggleDirection direction, bool force = false)
    {
        if (force == false && _currentDirection == direction)
            return;

        switch (direction)
        {
            case ToggleDirection.left:
                _switchLeft.Invoke();
                _spriteRenderer.sprite = _leftSprite;
                _currentDirection = ToggleDirection.left;
                break;
            case ToggleDirection.center:
                _switchCenter.Invoke();
                _spriteRenderer.sprite = _centerSprite;
                _currentDirection = ToggleDirection.center;
                break;
            case ToggleDirection.right:
                _switchRight.Invoke();
                _spriteRenderer.sprite = _rightSprite;
                _currentDirection = ToggleDirection.right;
                break;
            default:
                break;
        }
    }
}
