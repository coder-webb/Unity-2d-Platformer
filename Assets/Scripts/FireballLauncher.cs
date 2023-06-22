using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] Fireball _fireballPrefab;

    PlayerController _player;

    string _horizontalAxisInput;
    int _playerNumber;
    float _cooldownTimer;
    float _timeToWait = 1;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();
        _playerNumber = _player.PlayerNumber;
        _horizontalAxisInput = $"P{_playerNumber}Horizontal";
        _cooldownTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown($"Player{_playerNumber}Fire") && _cooldownTimer >= _timeToWait)
        {
            Fireball fireball = Instantiate(_fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = _player.FacingLeft ? -1f : 1f;

            _cooldownTimer = 0;
        }

        _cooldownTimer += Time.deltaTime;

    }
}
