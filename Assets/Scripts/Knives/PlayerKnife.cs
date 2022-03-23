using System;
using Knives;
using Scripts;
using UnityEngine;

public class PlayerKnife : KnifeBase
{
    public static event Action onLogHit;
    public static event Action onOtherKnifeHit;
    public static event Action onAppleHit;
    public static event Action onKnifeWasThrown;

    [SerializeField] private GameObject _logHitEffect;
    [SerializeField][Range(1,30f)]
    private float _throwingSpeed=25f;
    
    private bool _isThrown = false;


    private void Awake()
    {
        LogBase.onLogBreaking += ThrowAwayTheKnife;
        
        _rb = GetComponent<Rigidbody2D>();
        _hitBox = GetComponent<KnifeHitBox>();
    }

    private void Update()
    {
        if (_isThrown is false && Input.GetMouseButtonDown(0))
        {
            _isThrown = true;
            _rb.velocity = Vector2.up * _throwingSpeed;
            onKnifeWasThrown?.Invoke();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_isActive is false)
            return;

        _isActive = false;
        
        if (other.gameObject.CompareTag(KNIFE_TAG))
        {
            ThrowAwayTheKnife();
            onOtherKnifeHit?.Invoke();
        }
        if (other.gameObject.CompareTag(LogBase.LOG_TAG))
        {
            _rb.isKinematic = true;
            transform.SetParent(other.transform);
            _hitBox.MoveCollider();
            
            onLogHit?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Apple.APPLE_TAG))
        {
            onAppleHit?.Invoke();
        }
    }

    private void OnDisable()
    {
        LogBase.onLogBreaking -= ThrowAwayTheKnife;
    }
}
