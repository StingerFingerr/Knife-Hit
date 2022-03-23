using UnityEngine;

namespace Knives
{
    [RequireComponent(typeof(Rigidbody2D),typeof(KnifeHitBox))]
    public abstract class KnifeBase: MonoBehaviour
    {
        public const string KNIFE_TAG = "Knife";
        
        protected Rigidbody2D _rb;
        protected KnifeHitBox _hitBox;

        protected bool _isActive = true;

        protected void ThrowAwayTheKnife()
        {
            if (_isActive)
                return;

            _hitBox.DisableCollider();
            transform.SetParent(null);

            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0;

            _rb.isKinematic = false;
            _rb.gravityScale = 2f;

            _rb.AddForce(new Vector2(Random.Range(-2f,2f),1f) * 3, ForceMode2D.Impulse);
            _rb.AddTorque(Random.Range(-10f, 10f), ForceMode2D.Impulse);

            Destroy(gameObject, 2);
        }
    }
}