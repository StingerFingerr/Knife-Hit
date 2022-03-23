using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class ApplePart : MonoBehaviour
{
    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(Random.Range(-3f,3f),1f), ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-3f,3f), ForceMode2D.Impulse);
    }
}
