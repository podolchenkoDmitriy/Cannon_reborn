using _GAME.Scripts.Base;
using _GAME.Scripts.Components;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Cannon
{
    public class CannonVisualView : BaseView, IComponentInitializer
    {
        [SerializeField] private Transform shootPoint;
        private Material _material;
        private Tween _materialPlay;
        private static readonly int Filler = Shader.PropertyToID("_Filler");

        public void Initialize()
        {
            _material = GetComponentInChildren<MeshRenderer>().material;
        }
        
        public void PlayShoot()
        {
            _materialPlay?.Kill();
            SetMaterialProperty(0);
            _materialPlay = DOVirtual.Float(0, 1f, 0.3f, SetMaterialProperty);
        }

        private void SetMaterialProperty(float value)
        {
            _material.SetFloat(Filler, value);
        }

        public Transform GetShootPoint()
        {
            return shootPoint;
        }
    }
}
