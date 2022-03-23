using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LogHitLightEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _lightAnim;
    [SerializeField] [Range(.01f, .5f)] private float _durationAnim = .1f;
    
    private Light _light;

    private void Awake() => _light = GetComponent<Light>();


    private void OnEnable() => PlayerKnife.onLogHit += PlayLightEffect;
    private void OnDisable() => PlayerKnife.onLogHit -= PlayLightEffect;

    private void PlayLightEffect()
    {
        StartCoroutine(LightAnimation());
    }

    private IEnumerator LightAnimation()
    {
        float time = 0;
        _light.intensity = 0;
        
        while (time <= _durationAnim)
        {
            time += Time.deltaTime;

            _light.intensity = _lightAnim.Evaluate(time / _durationAnim);
            
            yield return null;
        }

        _light.intensity = 0;
    }
}
