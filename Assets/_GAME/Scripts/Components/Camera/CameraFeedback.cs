using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Components.Camera
{
    public class CameraFeedback : MonoBehaviour
    {
        [SerializeField] private Transform shakeCamera;
        private Tween _tween;
        
        public void ShakeCamera()
        {
            _tween?.Kill(true);
            _tween = shakeCamera.DOShakeRotation(0.3f, 0.5f);
        }
    }
}
