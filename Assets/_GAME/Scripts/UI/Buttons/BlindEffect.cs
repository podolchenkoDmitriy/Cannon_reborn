using _GAME.Scripts.Tools;
using _GAME.Scripts.Ui.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Scripts.Ui.Buttons
{
    public class BlindEffect : BaseUIView
    {
        [SerializeField] private Image _maskImage;
       
        private RectTransform _rect;
        private Image _image;
        private RectTransform _referenceRect;

        public BlindEffect Init(bool activate)
        {
            _rect ??= GetComponent<RectTransform>();
            if (!_image)
            {
                _image = GetComponent<Image>();
                _referenceRect = _maskImage != null ? _maskImage.rectTransform : null;
                _maskImage ??= transform.parent.GetComponent<Image>();
                if (!_maskImage)
                {
                    this.Deactivate();
                    return null;
                }
                if (_maskImage)
                {
                    
                    _image.sprite = _maskImage.sprite;
                    _image.pixelsPerUnitMultiplier = _maskImage.pixelsPerUnitMultiplier;
                }
            }
            UpdateRect();
            gameObject.SetActive(activate);

            return this;
        }

        private void UpdateRect()
        {
            _rect.sizeDelta = _referenceRect ? _referenceRect.sizeDelta : Vector2.zero;
            _rect.anchoredPosition = _referenceRect ? _referenceRect.anchoredPosition : Vector2.zero;
        }
    }
}
