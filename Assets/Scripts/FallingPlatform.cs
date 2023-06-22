using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;
    bool _falling = false;
    float _fallTimer;
    HashSet<PlayerController> _playersInTrigger = new HashSet<PlayerController>();
    Vector3 _startingPosition;
    Coroutine _coroutine;

    float _wiggleTimer = 0f;

    [Tooltip("Resets wiggle timer when no player is on the platform")]
    [SerializeField] bool _resetOnEmpty;
    [SerializeField] float _fallSpeed = 3;
    [SerializeField] float _wiggleDuration = 1;
    [Range(0.1f, 5f)] [SerializeField] float _fallAfterSeconds = 3;
    [Range(0.001f, 0.01f)] [SerializeField] float _shakeX = 0.005f;
    [Range(0.001f, 0.01f)] [SerializeField] float _shakeY = 0.005f;

    void Start()
    {
        _startingPosition = transform.position;
    }


    IEnumerator WiggleAndFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(.25f);
        Debug.Log("Wiggling");

        while (_wiggleTimer < _wiggleDuration)
        {
            float randomX = UnityEngine.Random.Range(-_shakeX, _shakeX);
            float randomY = UnityEngine.Random.Range(-_shakeY, _shakeY);
            transform.position = _startingPosition + new Vector3(randomX, randomY);
            float _wiggleInterval = UnityEngine.Random.Range(0.01f, 0.05f);
            _wiggleTimer += _wiggleInterval;
            yield return new WaitForSeconds(_wiggleInterval);
        }

        Debug.Log("Falling");

        _falling = true;

        foreach (var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }


        _fallTimer = 0;

        while (_fallTimer < _fallAfterSeconds)
        {
            transform.position += Vector3.down * Time.deltaTime * _fallSpeed;
            _fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player == null)
            return;

        _playersInTrigger.Add(player);

        PlayerInside = true;

        if (_playersInTrigger.Count > 0)
        {
            _coroutine = StartCoroutine(WiggleAndFall());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling)
            return;

        var player = collision.GetComponent<PlayerController>();
        if (player == null)
            return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(_coroutine);
            Debug.Log("Stopping Coroutine");

            if (_resetOnEmpty)
            {
                _wiggleTimer = 0;
            }
        }
    }
}
