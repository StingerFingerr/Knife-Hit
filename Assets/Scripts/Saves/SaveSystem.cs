using System;
using UnityEngine;

namespace Saves
{
    public class SaveSystem: MonoBehaviour
    {
        [System.Serializable] public class GameSettings
        {
            public bool VibrationIsOn;
            public bool SoundsIsOn;
            public bool LeftHandIsOn;

            public GameSettings(bool vibrationIsOn, bool soundsIsOn, bool leftHandIsOn)
            {
                VibrationIsOn = vibrationIsOn;
                SoundsIsOn = soundsIsOn;
                LeftHandIsOn = leftHandIsOn;
            }
        }
        [System.Serializable] public class GameResults
        {
            public int BestStage;
            public int BestScore;
            public int ApplesCount;
        }
        
        private const string SETTINGS = "SETTINGS";
        private const string RESULTS = "RESULTS";

        public static event Action<GameSettings> onInitGameSettings; 
        public static event Action<GameSettings> onSettingsChanged;

        public static event Action<GameResults> onInitGameResults;

        public static GameSettings CurrentSettings;
        public static GameResults CurrentResults;

        public static GameSettings UpdatedSettings => UpdateSettings();
        public static GameResults UpdatedResults => UpdateResults();
        
        private void Awake()
        {
            if (PlayerPrefs.HasKey(SETTINGS) is false)
                PlayerPrefs.SetString(SETTINGS ,JsonUtility.ToJson(new GameSettings(true,true,false)));
            
            if (PlayerPrefs.HasKey(RESULTS) is false)
                PlayerPrefs.SetString(RESULTS, JsonUtility.ToJson(new GameResults()));
                
        }

        private void Start()
        {
            onInitGameSettings?.Invoke(UpdateSettings());
            onInitGameResults?.Invoke(UpdateResults());
        }

        public void SwitchSounds(bool soundIsOn)
        {
            CurrentSettings.SoundsIsOn = soundIsOn;
            SaveGameSettings();
            onSettingsChanged?.Invoke(CurrentSettings);
        }

        public void SwitchVibrations(bool vibrationIsOn)
        {
            CurrentSettings.VibrationIsOn = vibrationIsOn;
            SaveGameSettings();
            onSettingsChanged?.Invoke(CurrentSettings);
        }

        public void SwitchIsLeftHand(bool leftHandIsOn)
        {
            CurrentSettings.LeftHandIsOn = leftHandIsOn;
            SaveGameSettings();
            onSettingsChanged?.Invoke(CurrentSettings);
        }

        private static void SaveGameSettings()
        {
            PlayerPrefs.SetString(SETTINGS, JsonUtility.ToJson(CurrentSettings));
        }

        private static void SaveGameResults()
        {
            PlayerPrefs.SetString(RESULTS, JsonUtility.ToJson(CurrentResults));
        }

        public static void SaveGameResults(int apples, int score, int stage)
        {
            if (score > CurrentResults.BestScore)
                CurrentResults.BestScore = score;
            if (stage > CurrentResults.BestStage)
                CurrentResults.BestStage = stage;
            
            CurrentResults.ApplesCount = apples;
            SaveGameResults();
        }

        private static GameSettings UpdateSettings()
        {
            CurrentSettings = JsonUtility.FromJson<GameSettings>(PlayerPrefs.GetString(SETTINGS));
            return CurrentSettings;
        }

        private static GameResults UpdateResults()
        {
            CurrentResults = JsonUtility.FromJson<GameResults>(PlayerPrefs.GetString(RESULTS));
            return CurrentResults;
        }
        
    }
}