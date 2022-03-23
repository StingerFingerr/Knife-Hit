using System;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(LogMotor), typeof(CircleCollider2D))]
    public abstract class LogBase: MonoBehaviour
    {
        public const string LOG_TAG = "Log";

        public static event Action onLogBreaking;
        
        [SerializeField] protected Transform _normalLogTransform;
        [SerializeField] protected GameObject _brokenLogPrefab;
        protected LogMotor _logMotor;
        
        
        public virtual void BreakLog()
        {
            GetComponent<LogMotor>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            
            _normalLogTransform.gameObject.SetActive(false);
            Instantiate(_brokenLogPrefab, transform);
            
            onLogBreaking.Invoke();
            
            Destroy(gameObject, 2);
        }
    }
}