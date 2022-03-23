using Saves;
using Scripts;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    private readonly long[] _roundWonPattern = {200, 200, 200, 200};

    private bool _isCanVibrate = true;
    
    
    private void Awake() => Vibration.Init();

    private void OnEnable()
    {
        PlayerKnife.onLogHit += SuccessfulHitVibration;
        PlayerKnife.onOtherKnifeHit += GameOverVibration;
        LogBase.onLogBreaking += RoundWonVibration;

        SaveSystem.onInitGameSettings += SwitchVibration;
        SaveSystem.onSettingsChanged += SwitchVibration;
    }

    private void OnDisable()
    {
        PlayerKnife.onLogHit -= SuccessfulHitVibration;
        PlayerKnife.onOtherKnifeHit -= GameOverVibration;
        LogBase.onLogBreaking -= RoundWonVibration;
        
        SaveSystem.onInitGameSettings -= SwitchVibration;
        SaveSystem.onSettingsChanged -= SwitchVibration;
    }

    private void SwitchVibration(SaveSystem.GameSettings gameSettings)
    {
        _isCanVibrate = gameSettings.VibrationIsOn;
    }

    private void SuccessfulHitVibration()
    {
        if(_isCanVibrate)
            Vibration.VibratePop();
    }

    private void GameOverVibration()
    {
        if(_isCanVibrate)
            Vibration.Vibrate(200);
    }

    private void RoundWonVibration()
    {
        if(_isCanVibrate)
            Vibration.Vibrate(_roundWonPattern,-1 );
    }
}
