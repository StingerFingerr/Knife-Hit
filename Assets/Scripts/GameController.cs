using System.Threading.Tasks;
using Saves;
using Scripts;
using UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private GameUI _gameUI;

    private int _delayBetweenRounds = 1000;

    private int _score = 0;
    private int _stage = 1;
    private int _level = 0;

    private int _applesCount=0;

    private int _remainingPlayerKnives;
    private LogBase _currentLog;
    
    private void OnEnable()
    {
        PlayerKnife.onLogHit += SuccessfulHit;
        PlayerKnife.onOtherKnifeHit += UnSuccessfulHit;
        PlayerKnife.onAppleHit += IncreaseApplesCount;
        PlayerKnife.onKnifeWasThrown += DecreaseRemainingKnives;

        RoundManager.onNextRoundPrepared += StartRound;

        SaveSystem.onInitGameResults += InitApples;
    }


    private void InitApples(SaveSystem.GameResults gameResults)
    {
        _applesCount = gameResults.ApplesCount;
    }
    
    public void StartGame()
    {
        _currentLog = _roundManager.InitNewRound(_level, _stage);
        _gameUI.SetScore(0);
    }

    public void RestartGame()
    {
        _score = 0;
        _stage = 1;
        _level = 0;
        
        _gameUI.RestartGame();

        StartGame();
    }

    private void StartRound(int playerKnivesCount)
    {
        _remainingPlayerKnives = playerKnivesCount;
        
        _gameUI.SetStage(_stage);
        _gameUI.SetAvailableKnives(_remainingPlayerKnives);
        
        CreateKnife();
    }

    private void DecreaseRemainingKnives()
    {
        _remainingPlayerKnives--;
        _gameUI.SetRemainingKnives(_remainingPlayerKnives);
    }

    private void IncreaseApplesCount()
    {
        _applesCount+=2;
        _gameUI.SetApples(_applesCount);
    }
    
    private void SuccessfulHit()
    {
        _score++;
        _gameUI.SetScore(_score);
        
        if(_remainingPlayerKnives >0)
        {
            CreateKnife();
            return;
        }
        
        _stage++;
        _level = _stage / 5;
        
        _currentLog.BreakLog();

        InitNewRoundWithDelay(_delayBetweenRounds);

    }

    private async Task InitNewRoundWithDelay(int delayMilliSec)
    {
        await Task.Delay(delayMilliSec);
        
        _currentLog = _roundManager.InitNewRound(_level, _stage);
    }
    
    private void UnSuccessfulHit()
    {
        Destroy(_currentLog.gameObject);
        
        SaveSystem.SaveGameResults(_applesCount, _score, _stage);
        
        _gameUI.OpenGameOverScreen(_stage, _score);
    }

    private void CreateKnife()
    {
        Instantiate(_knifePrefab);
    }

    private void OnDisable()
    {
        PlayerKnife.onLogHit -= SuccessfulHit;
        PlayerKnife.onOtherKnifeHit -= UnSuccessfulHit;
        PlayerKnife.onAppleHit -= IncreaseApplesCount;
        PlayerKnife.onKnifeWasThrown -= DecreaseRemainingKnives;
        
        RoundManager.onNextRoundPrepared -= StartRound;
        
        SaveSystem.onInitGameResults -= InitApples;
    }
}
