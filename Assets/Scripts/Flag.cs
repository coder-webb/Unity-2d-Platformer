using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] string _nextSceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent("PlayerController");
        var animator = GetComponent<Animator>();

        if (player == null)
        {
            return;
        }

        StartCoroutine(LoadAfterDelay());

        animator.SetTrigger("Raise");
        // Play flag animation
        // Load new level

    }

    IEnumerator LoadAfterDelay()
    {
        PlayerPrefs.SetInt(_nextSceneName + "Unlocked", 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_nextSceneName);
    }
}
