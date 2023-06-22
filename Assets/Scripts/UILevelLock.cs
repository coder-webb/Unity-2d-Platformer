using UnityEngine;

public class UILevelLock : MonoBehaviour
{
    void OnEnable()
    {
        var startButton = GetComponent<UIStartButton>();
        var key = startButton.LevelName + "Unlocked"; // Level1Unlocked string
        var unlockCheck = PlayerPrefs.GetInt(key, 0);

        if (unlockCheck == 0)
        {
            gameObject.SetActive(false);
        }
    }
    
    [ContextMenu("Delete Level Unlock")]
    void DeleteLevelLockKey()
    {
        var startButton = GetComponent<UIStartButton>();
        var key = startButton.LevelName + "Unlocked"; // Level1Unlocked string
        PlayerPrefs.DeleteKey(key);
    }
}
