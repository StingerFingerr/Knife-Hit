using Scripts;
using UnityEngine;

namespace Knives
{
    public class StarterKnife: KnifeBase
    {
        private void Awake()
        {
            LogBase.onLogBreaking += ThrowAwayTheKnife;

            _rb = GetComponent<Rigidbody2D>();
            _hitBox = GetComponent<KnifeHitBox>();

            _isActive = false;
        }

        private new void ThrowAwayTheKnife()
        {
            transform.position = transform.GetChild(0).position;
            transform.GetChild(0).localPosition = Vector3.zero;
            
            base.ThrowAwayTheKnife();
        }
        
        private void OnDisable()
        {
            LogBase.onLogBreaking -= ThrowAwayTheKnife;
        }
    }
}