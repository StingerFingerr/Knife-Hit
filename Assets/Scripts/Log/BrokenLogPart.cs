using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class BrokenLogPart: MonoBehaviour
    {
        private void Start()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            
            rb.AddForce(new Vector2(Random.Range(-3f, 3f), Random.Range(1f, 4f)), ForceMode.Impulse);
            rb.AddTorque(Random.Range(-1f,1f),Random.Range(-1f,1f),Random.Range(-1f,1f),ForceMode.Impulse);
        }
    }
}