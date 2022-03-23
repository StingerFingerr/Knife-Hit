using UnityEngine;

namespace Scripts
{
    public class BossLog: LogBase
    {
        [SerializeField][Range(1,20)] private int _playerKnivesCount;
        public int PlayerKnivesCount => _playerKnivesCount;
    }
}