using _GAME.Scripts.Base;
using _GAME.Scripts.Systems;
using _GAME.Scripts.Systems.Tick;
using UnityEngine;

namespace _GAME.Scripts.Cannon
{
    public class CannonController : BaseView,ITickableSystemClaimer,ITickableComponent
    {
        [SerializeField] private float sensitivity = 2.0f;
        [SerializeField] private float clampY = 60f;
        [SerializeField] private float clampX = 60f;
        
        public void ClaimTickableSystem(TickableSystem tickableSystem)
        {
            tickableSystem.AddToList(this);
        }

        public void Tick(float time)
        {
            if (Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = -Input.GetAxis("Mouse Y");

                float rotationY = transform.localEulerAngles.y + mouseX * sensitivity;

                float rotationX = transform.localEulerAngles.x + mouseY * sensitivity;
                rotationX = Mathf.Clamp(rotationX, clampX, 360);
                rotationY = Mathf.Clamp(rotationY, -clampY, clampY);

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
        }
    }
}
