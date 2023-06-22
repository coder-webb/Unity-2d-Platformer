using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    // You can't serialize hashsets. Hashsets don't allow duplicates. To remove duplicates immediately you can use a list and the .Distinct().ToList() to remove duplicates
    [SerializeField] public List<Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;

    TMP_Text _collectiblesRemainingText;
    int _collectiblesRemaining;

    public int _collectiblesCollected;

    // Start is called before the first frame update
    void Start()
    {
        _collectiblesRemainingText = GetComponentInChildren<TMP_Text>();
        _collectiblesRemaining = _collectibles.Count;
        _collectiblesRemainingText?.SetText(_collectiblesRemaining.ToString());

        foreach (var collectible in _collectibles)
        {
            collectible.OnPickedUp += UpdateCountRemaining;
        }
    }

    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }

    // Update is called once per frame
    public void UpdateCountRemaining()
    {
        _collectiblesCollected++;
        _collectiblesRemaining--;

        // The ? only works for calling a method. Essentially says if collectiblesRemainingText != null, then set text
        _collectiblesRemainingText?.SetText(_collectiblesRemaining.ToString());

        if (_collectiblesRemaining > 0)
            return;

        _onCollectionComplete.Invoke();
        Debug.Log("All gems collected");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        foreach (var collectible in _collectibles)
            Gizmos.DrawLine(transform.position, collectible.transform.position);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var collectible in _collectibles)
            Gizmos.DrawLine(transform.position, collectible.transform.position);
    }

}
