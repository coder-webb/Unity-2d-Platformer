using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] string _playerPrefText;
    private void OnEnable()
    {
        var key = PlayerPrefs.GetInt(_playerPrefText).ToString();
        GetComponent<TMP_Text>().SetText(key);
    }
}
