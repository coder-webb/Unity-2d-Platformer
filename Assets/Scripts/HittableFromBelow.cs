using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite _usedSprite;

    protected virtual bool CanUse => true;

    void OnCollisionEnter2D(Collision2D collision)
    {
         if (!CanUse)
            return;

        var player = collision.collider.GetComponent<PlayerController>();
        if (player == null)
            return;

        if (collision.contacts[0].normal.y > 0)
        {
            Use();

            var hitAnimation = GetComponent<Animator>();
            
            if (hitAnimation != null)
                hitAnimation.SetTrigger("Use");
        }

        if (!CanUse)
            GetComponent<SpriteRenderer>().sprite = _usedSprite;
    }

    protected virtual void Use()
    {
        Debug.Log($"Used {gameObject.name}");
    }
}
