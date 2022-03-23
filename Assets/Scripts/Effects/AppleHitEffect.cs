using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AppleHitEffect: MonoBehaviour
    {
        private ParticleSystem _ps;

        private void Awake() => _ps = GetComponent<ParticleSystem>();

        private void OnEnable() => PlayerKnife.onAppleHit += PlayAppleHitEffect;
        private void OnDisable() => PlayerKnife.onAppleHit -= PlayAppleHitEffect;

        private void PlayAppleHitEffect() => _ps.Play();
    }
}