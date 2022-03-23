using Saves;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI: MonoBehaviour
    {
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _gamePanel;

        [SerializeField] private KnivesHolder _knivesHolder;
        [SerializeField] private RoundMarkersHolder _roundMarkers;
        [SerializeField] private Text _gameScoreText;

        [SerializeField] private Text _bestScoreText;
        [SerializeField] private Text _bestStageText;
        [SerializeField] private Text _applesCountText;

        [SerializeField] private Text _scoreResults;
        [SerializeField] private Text _stageResults;

        private void OnEnable() => SaveSystem.onInitGameResults += InitGameResults;

        private void OnDisable() => SaveSystem.onInitGameResults -= InitGameResults;

        private void InitGameResults(SaveSystem.GameResults gameResults)
        {
            _bestScoreText.text = $"SCORE {gameResults.BestScore}";
            _bestStageText.text = $"STAGE {gameResults.BestStage}";
            _applesCountText.text = gameResults.ApplesCount.ToString();
        }

        public void ExitToMenu()
        {
            _gameOverPanel.SetActive(false);
            _menuPanel.SetActive(true);
            InitGameResults(SaveSystem.UpdatedResults);
        }

        public void SetAvailableKnives(int allKnives) => _knivesHolder.SetAvailableKnives(allKnives);
        public void SetRemainingKnives(int remainingKnives) => _knivesHolder.SetRemainingKnives(remainingKnives);
        
        public void SetStage(int stage) => _roundMarkers.SetStage(stage);
        public void SetScore(int score) => _gameScoreText.text = score.ToString();
        public void SetApples(int apples) => _applesCountText.text = apples.ToString();

        public void OpenGameOverScreen(int stage, int score)
        {
            _gamePanel.SetActive(false);
            _gameOverPanel.SetActive(true);

            _stageResults.text = $"STAGE {stage}";
            _scoreResults.text = $"SCORE {score}";
        }
        public void RestartGame()
        {
            _gamePanel.SetActive(true);
        }
    }
}