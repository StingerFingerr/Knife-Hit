using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RoundMarkersHolder: MonoBehaviour
    {
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _passedColor;

        [SerializeField] private Text _stageText;

        private Image[] _markers;
        
        public void SetStage(int stage)
        {
            if (_markers is null)
                _markers = transform.GetComponentsInChildren<Image>();

            _stageText.text = $"STAGE {stage}";

            int round = stage % 5;
            if (round == 0)
                round = 5;

            for (int i = 0; i < round; i++)
            {
                _markers[i].color = _passedColor;
            }

            for (int i =round; i < 5; i++)
            {
                _markers[i].color = _normalColor;
            }
            
            
        }
    }
}