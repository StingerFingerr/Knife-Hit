using System.Collections;
using UnityEngine;

public class CameraShakeEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _shakeAnim;
    [SerializeField][Range(.01f,.5f)] private float _durationAnim = .1f;
    

    private void OnEnable() => PlayerKnife.onLogHit += PlayCameraEffect;
    private void OnDisable() => PlayerKnife.onLogHit -= PlayCameraEffect;


    private void PlayCameraEffect()
    {
        StartCoroutine(ShakeAnimation());
    }

    private IEnumerator ShakeAnimation()
    {
        float time = 0;
        Vector3 startPos = transform.position;

        while (time <= _durationAnim)
        {
            time += Time.deltaTime;

            transform.position = startPos - Vector3.up * _shakeAnim.Evaluate(time / _durationAnim);
            
            yield return null;
        }

        transform.position = startPos;
    }
}
