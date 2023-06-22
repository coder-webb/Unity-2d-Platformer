using UnityEngine;
using UnityEngine.Events;

    
public class KeyLock : MonoBehaviour
{
    [SerializeField] UnityEvent _onUnlocked;
    [SerializeField] public string _lockColor;

    public void Unlock()
    {
        Debug.Log("Unlocked");
        _onUnlocked.Invoke();
    }
}
