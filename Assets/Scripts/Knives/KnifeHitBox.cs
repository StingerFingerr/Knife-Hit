using UnityEngine;

[RequireComponent( typeof(BoxCollider2D))]
public class KnifeHitBox : MonoBehaviour
{
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void MoveCollider()
    {
        _boxCollider.offset = new Vector2(0, -.2f);
        _boxCollider.size = new Vector2(.15f, .6f);
    }

    public void DisableCollider()
    {
        _boxCollider.enabled = false;
    }
}

