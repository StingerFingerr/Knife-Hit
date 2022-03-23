using UnityEngine;
using UnityEngine.UI;

namespace Saves
{
    [RequireComponent(typeof(Toggle))]
    public class VibrationCheckBox: MonoBehaviour
    {
        private void Awake() => GetComponent<Toggle>().isOn = SaveSystem.CurrentSettings.VibrationIsOn;
    }
}