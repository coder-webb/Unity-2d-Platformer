using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIStartButton : MonoBehaviour
{
    [SerializeField] string _sceneToLoad;

    public string LevelName => _sceneToLoad; // A getter for the _scene to load string

    public void StartButton()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
