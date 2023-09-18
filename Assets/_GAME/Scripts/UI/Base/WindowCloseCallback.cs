using _GAME.Scripts.Tools;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _GAME.Scripts.UI.Base
{
    public class WindowCloseCallback : MonoBehaviour,IPointerDownHandler
    {
        public UnityAction<WindowCloseCallback> Callback;
        private Image _fadeImage;
        public void OnPointerDown(PointerEventData eventData)
        {
            Callback?.Invoke(this);
            _fadeImage.DOFade(0, 0.1f).OnComplete(() => this.Deactivate());
        }

        public void Init()
        {
            _fadeImage = GetComponent<Image>();
        }
    }
}
