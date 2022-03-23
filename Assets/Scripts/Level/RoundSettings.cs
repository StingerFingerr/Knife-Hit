using UnityEngine;

namespace Level
{
    [CreateAssetMenu( menuName = "Game settings/Round settings", fileName = "Round settings", order = 1)]
    public class RoundSettings: ScriptableObject
    {
        [SerializeField] [Range(0f, 1f)] private float _appleSpawnChance;
        [SerializeField] [Range(5, 10)] private int _maxStarterKnivesCount;
        [SerializeField] [Range(0, 5)] private int _minStarterKnivesCount;
        
        public float AppleSpawnChance => _appleSpawnChance;
        public int MaxStarterKnivesCount => _maxStarterKnivesCount;
        public int MinStarterKnivesCount => _minStarterKnivesCount;
    }
}