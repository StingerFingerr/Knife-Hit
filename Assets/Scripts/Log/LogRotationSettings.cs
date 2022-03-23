using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Game settings/Log rotation settings",fileName = "LogRotationSettings", order = 1)]
    public class LogRotationSettings: ScriptableObject
    {
        public AnimationCurve rotationSpeedModifier;

        [Range(.1f, 50f)] public float maxRotationSpeed = 1f;

        [Range(.1f, 50f)] public float duration;
    }
}