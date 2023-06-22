using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidbody2d;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    Collider2D hit;

    Vector2 _startPosition;

    [SerializeField] int _playerNumber = 1;
    [Header("Movement")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _slideDuration = 1;
    [Header("Jump")]
    [SerializeField] float _jumpVelocity = 10f;
    [SerializeField] float _downPull = .2f;
    [SerializeField] float _maxJumpDuration = .1f;
    [SerializeField] float _maxFallingVelocity;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Transform _feet;
    [SerializeField] float rayDistance = 0.06f; // TESTING ONLY
    [Header("Wall Sliding")]
    [SerializeField] float _wallSlideSpeed = 2f;
    [SerializeField] Transform _playerLeft;
    [SerializeField] Transform _playerRight;

    int _jumpsRemaining;
    int _defaultLayerMask;

    float _horizontal;
    float _fallTimer;
    float _jumpTimer;

    public bool FacingLeft { get; private set; }

    bool WallSliding;
    bool _isGrounded;
    bool _isOnSlipperySurface;
    bool _sliding;
    bool _jumping;

    string _jumpButton;
    string _horizontalAxisInput;

    public int PlayerNumber => _playerNumber; // same as public int PlayerNumber { get { _playerNumber; } }


    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _startPosition = transform.position;
        _jumpsRemaining = _maxJumps;
        _jumpButton = $"P{_playerNumber}Jump";
        _horizontalAxisInput = $"P{_playerNumber}Horizontal";
        _defaultLayerMask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsGrounded();

        ReadHorizontalInput();
        if (_isOnSlipperySurface)
            SlipHorizontal();
        else 
            MoveHorizontal();

        if (ShouldSlide())
            if (ShouldStartJump())
            {
                WallJump();
                _sliding = false;
            }
            else
            {
                Slide();
            }
        else
            _sliding = false;

        if (ShouldStartJump())
            Jump();
        else if (ShouldContinueJump())
            ContinueJump();

        UpdateAnimator();
        UpdateSpriteDirection();

        if (_isGrounded && _fallTimer > 0)
        {
            _jumpsRemaining = _maxJumps;
            _fallTimer = 0;
        }
        else
        {
            FallingVelocity();
        }

        _jumpTimer += Time.deltaTime;
    }

    void WallJump()
    {
        _rigidbody2d.velocity = new Vector2(-(_horizontal * _moveSpeed), _jumpVelocity * 1.5f);
    }

    void Slide()
    {
        _sliding = true;
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, -_wallSlideSpeed);
    }

    bool ShouldSlide()
    {
        if (_isGrounded)
            return false;

        if (_rigidbody2d.velocity.y > 0)
            return false;

        var hitLeft = Physics2D.OverlapCircle(_playerLeft.transform.position, rayDistance);
        if (hitLeft != null && hitLeft.CompareTag("Wall") && _horizontal < 0)
            return true;

        var hitRight = Physics2D.OverlapCircle(_playerRight.transform.position, rayDistance);
        if (hitRight != null && hitRight.CompareTag("Wall") && _horizontal > 0)
            return true;

        return false;
    }

    void FallingVelocity()
    {
        if (_rigidbody2d.velocity.y <= -_maxFallingVelocity)
            return;
        else
        {
            var _downForce = _downPull * (_fallTimer * _fallTimer);
            _fallTimer += Time.deltaTime;
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _rigidbody2d.velocity.y - _downForce);
        }
    }

    void ContinueJump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpVelocity);
        _fallTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    void Jump()
    {
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, _jumpVelocity);
        _jumpsRemaining--;
        _jumping = true;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    bool ShouldStartJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    void MoveHorizontal()
    {
        var newHorizontal = Mathf.Lerp(_rigidbody2d.velocity.x, _horizontal * _moveSpeed, Time.deltaTime);

        _rigidbody2d.velocity = new Vector2(newHorizontal, _rigidbody2d.velocity.y);
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal, _rigidbody2d.velocity.y);
        var smoothedVelocity = Vector2.Lerp(_rigidbody2d.velocity, desiredVelocity, Time.deltaTime / _slideDuration);
        /* For Lerp (linear interpolation), the start and end should be fixed, and t should have a definite end. */
        _rigidbody2d.velocity = smoothedVelocity;
    }

    void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxisInput) * _moveSpeed;
    }

    void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer.flipX = _horizontal < 0;
            FacingLeft = _spriteRenderer.flipX;
        }
    }

    void UpdateAnimator()
    {
        var _walking = _horizontal != 0;
        _animator.SetBool("Walk", _walking);
        _animator.SetBool("Jump", ShouldStartJump());
        _animator.SetBool("Sliding", _sliding);
    }

    void UpdateIsGrounded()
    {
        hit = Physics2D.OverlapCircle(_feet.position, rayDistance, _defaultLayerMask);
        Debug.DrawRay(_feet.position, Vector3.down * rayDistance);
        _isGrounded = hit != null;

        if (hit != null)
        {
            _jumping = false;
            if (hit.CompareTag("Slippery"))
                _isOnSlipperySurface = true;
        }
        else
            _isOnSlipperySurface = false;
    }

    internal void ResetToStart()
    {
        _rigidbody2d.position = _startPosition;
    }

    internal void TeleportTo(Vector3 position)
    {
        _rigidbody2d.position = position;
        _rigidbody2d.velocity = Vector2.zero;
    }
}
