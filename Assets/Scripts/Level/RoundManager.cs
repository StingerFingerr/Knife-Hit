using System;
using Level;
using Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoundManager : MonoBehaviour
{
    public static event Action<int> onNextRoundPrepared;

    [SerializeField] private RoundSettings _roundSettings;
    [SerializeField] private GameObject _starterKnifePrefab;
    [SerializeField] private GameObject _applePrefab;
    
    [SerializeField] private GameObject[] _logsPrefabs;
    [SerializeField] private GameObject[] _bossesPrefabs;
    
    
    private int[] _defaultPlayerKnivesCount;

    private RoundSettings _currentRoundSettings;
    private GameObject _currentLogPrefab;
    
    private int _playerKnivesCount;

    private void Awake()
    {
        _defaultPlayerKnivesCount = new[] {6, 8, 8, 7, 5, 6, 4, 3, 3, 2, 2};
    }

    public LogBase InitNewRound(int level, int stage)
    {
        LogBase logBase;
        if(stage%5==0)
        {
            logBase = SpawnBoss(level);
        }
        else
        {
            if(stage%5==1)
                ReInitCurrentLog();
            logBase = SpawnLog(level);
        }

        onNextRoundPrepared?.Invoke(_playerKnivesCount);
        
        return logBase;
    }

    private void ReInitCurrentLog()
    {
        int ind = Random.Range(0, _logsPrefabs.Length);
        _currentLogPrefab = _logsPrefabs[ind];
    }
    
    private LogBase SpawnLog(int level)
    {
        GameObject log = Instantiate(_currentLogPrefab, transform.position, Quaternion.identity);
        SetDifficulty(level, log.transform);
        return log.GetComponent<StandardLog>();
    }

    private LogBase SpawnBoss(int level)
    {
        int ind = Random.Range(0, _bossesPrefabs.Length);
        BossLog boss = Instantiate(_bossesPrefabs[ind], transform.position, Quaternion.identity)
            .GetComponent<BossLog>();
        _playerKnivesCount = boss.PlayerKnivesCount;
        return boss;
    }

    private void SetDifficulty(int level, Transform log)
    {
        int sk = SetStarterKnives(log);
        SetApple(sk, log);
        SetPlayerKnives(sk, level);
    }

    private int SetStarterKnives(Transform log)
    {
        int starterKnivesCount = Random.Range(
            _roundSettings.MinStarterKnivesCount,
            _roundSettings.MaxStarterKnivesCount);
        
        float rot = 360f / starterKnivesCount;

        for (int i = 0; i < starterKnivesCount; i++)
        {
            Instantiate(_starterKnifePrefab, transform.position, Quaternion.Euler(0, 0, rot * i))
                .transform.SetParent(log.transform);
        }
        
        return starterKnivesCount;
    }

    private void SetApple(int starterKnivesCount, Transform log)
    {
        if(Random.Range(0f,1f)> _roundSettings.AppleSpawnChance)
            return;
        
        Instantiate(_applePrefab,log)
            .transform.rotation = Quaternion.Euler(0,0,360f/starterKnivesCount/2f);
    }
    
    private void SetPlayerKnives(int starterKnives, int level)
    {
        _playerKnivesCount = _defaultPlayerKnivesCount[starterKnives] + level * 2;
    }
}
