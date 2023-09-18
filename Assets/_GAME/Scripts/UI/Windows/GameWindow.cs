using _GAME.Scripts.Trajectory;
using _GAME.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _GAME.Scripts.UI.Windows
{
    public class GameWindow : BaseWindow
    {
        public UnityAction<float> OnChangeValue;
        
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI powerText;
        private int _maxValue = 0;

        public override void Init()
        {
            base.Init();
            slider.onValueChanged.AddListener(OnChangeSliderValue);
            _maxValue = (int)TrajectoryCalculation.GetMaxPower();
        }

        private void OnChangeSliderValue(float value)
        {
            OnChangeValue?.Invoke(value);
            float power = value * _maxValue;
            powerText.text =power.ToString("0");
        }

        public void SimulatePower(float power)
        {
            slider.value = power;
        }
    }
}