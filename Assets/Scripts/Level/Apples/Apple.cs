using Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Apple : MonoBehaviour
{
    public const string APPLE_TAG = "Apple";
    [SerializeField] private GameObject _slicedApple;

    private void OnEnable()
    {
        LogBase.onLogBreaking += ThrowAwayTheApple;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerKnife.KNIFE_TAG))
        {
            SliceApple();
        }
    }

    private void SliceApple()
    {
        GameObject slicedApple = Instantiate(_slicedApple, transform.GetChild(0).position, transform.rotation);
        Destroy(slicedApple,2f);
        Destroy(gameObject);
    }

    private void ThrowAwayTheApple()
    {
        transform.position = transform.GetChild(0).position;
        transform.GetChild(0).localPosition = Vector3.zero;
        
        GetComponent<BoxCollider2D>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = false;
        rb.gravityScale = 1;
        rb.AddForce(new Vector2(Random.Range(-3f,3f),1f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-3f,3f), ForceMode2D.Impulse);

        Destroy(gameObject, 2);
    }
    
    private void OnDisable()
    {
        LogBase.onLogBreaking -= ThrowAwayTheApple;
    }
}
