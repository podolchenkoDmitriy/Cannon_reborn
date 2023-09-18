using _GAME.Scripts.Systems;
using _GAME.Scripts.Systems.Tick;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _GAME.Scripts.InputListener
{
    public class InputListener : MonoBehaviour, ITickableComponent, ITickableSystemClaimer
    {
        public static UnityAction OnTouchBegan;
        public static UnityAction<Vector2> OnTouchMoved;
        private Vector2 _previousTouchPosition;
        public void Tick(float time)
        {
            if (EventSystem.current.IsPointerOverGameObject()) 
            {
                return;
            }
#if UNITY_EDITOR
            
            if (Input.GetMouseButtonDown(0))
            {
                OnTouchBegan?.Invoke();
            }
            else if (Input.GetMouseButton(0))
            {
                var x = Input.GetAxis("Mouse X");
                var y = -Input.GetAxis("Mouse Y");
                Vector2 direction = new Vector2(x, y);

                OnTouchMoved?.Invoke(direction);
            }
            return;
#endif
            if (Input.touchCount ==1)
            {
                Vector2 touchDelta = Vector2.zero;

                var touch = Input.GetTouch(0); 
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegan?.Invoke();
                        break;
                    case TouchPhase.Moved:

                        touchDelta =  touch.deltaPosition*time;
                        
                        OnTouchMoved?.Invoke(touchDelta);
                        break;
                }
            }
            else if (Input.touchCount > 1)
            {
                var touch = Input.GetTouch(1);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegan?.Invoke();
                        break;
                }
            }
        }

        public void ClaimTickableSystem(TickableSystem tickableSystem)
        {
            tickableSystem.AddToList(this);
        }
    }
}
