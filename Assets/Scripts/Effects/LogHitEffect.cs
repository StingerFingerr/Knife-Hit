using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class LogHitEffect: MonoBehaviour
    {
        private ParticleSystem _ps;

        private void Awake() => _ps = GetComponent<ParticleSystem>();

        private void OnEnable() => PlayerKnife.onLogHit += PlayLogHitEffect;
        private void OnDisable() => PlayerKnife.onLogHit -= PlayLogHitEffect;

        private void PlayLogHitEffect() => _ps.Play();
    }
}