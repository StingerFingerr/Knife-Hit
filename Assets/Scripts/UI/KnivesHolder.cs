using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class KnivesHolder: MonoBehaviour
    {
        [SerializeField] private Color _invisibleColor;
        [SerializeField] private Color _availableColor;
        [SerializeField] private Color _transparentColor;
        
        private Image[] _knives;
        private int _knivesInHolder;
        private int _availableKnives;
        

        public void SetAvailableKnives(int count)
        {
            if (_knives is null)
            {
                _knives = transform.GetComponentsInChildren<Image>();
                _knivesInHolder = _knives.Length;
            }
            
            _availableKnives = count;
            for (int i = 0; i < _knivesInHolder-count; i++)
            {
                _knives[i].color = _invisibleColor;
            }

            for (int i = _knivesInHolder-count; i < _knivesInHolder; i++)
            {
                _knives[i].color = _availableColor;
            }
        }

        public void SetRemainingKnives(int count)
        {
            for (int i = _knivesInHolder - _availableKnives; i < _knivesInHolder - count; i++)
            {
                _knives[i].color = _transparentColor;
            }
        }
    }
}