using Scripts;
using UnityEngine;

public class LogMotor : MonoBehaviour
{
    [SerializeField] private LogRotationSettings[] _rotationSettings;
    private LogRotationSettings _currentRS;

    
    private void Awake()
    {
        _currentRS = _rotationSettings[Random.Range(0,_rotationSettings.Length)];
    }


    void FixedUpdate()
    {
        float rotationSpeed = _currentRS.rotationSpeedModifier.
            Evaluate((Time.time% _currentRS.duration)/ _currentRS.duration) * _currentRS.maxRotationSpeed;
        
        transform.Rotate(
            transform.forward, rotationSpeed);



    }
}
