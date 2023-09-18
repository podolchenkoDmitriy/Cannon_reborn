using _GAME.Scripts.Base;
using _GAME.Scripts.Components;
using UnityEngine;

namespace _GAME.Scripts.Cannon
{
    public class CannonController : BaseView, IComponentInitializer
    {
        [SerializeField] private float sensitivity = 2.0f;
        [SerializeField] private float clampY = 60f;
        [SerializeField] private float clampX = 60f;

        private void CannonMove(Vector2 direction)
        {

            var mouseX = direction.x;
            var mouseY = -direction.y;
            var rotationY = transform.localEulerAngles.y + mouseX * sensitivity;

            var rotationX = transform.localEulerAngles.x + mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, clampX, 359);
            rotationY = Mathf.Clamp(rotationY, -clampY, clampY);

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }

        public void Initialize()
        {
            InputListener.InputListener.OnTouchMoved += CannonMove;
        }
    }
}