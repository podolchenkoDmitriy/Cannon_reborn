using System;
using System.Collections.Generic;
using System.Linq;
using _GAME.Scripts.Base;
using _GAME.Scripts.Tools;
using _GAME.Scripts.UI.WorldSpace;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Particles
{
    public class ParticlesView : BaseView
    {
        [SerializeField] private List<ParticleConfig> configs;
        private ParticleConfig _activeConfig;
        public void SetupItemView(ParticleType type)
        {
            _activeConfig = configs.FirstOrDefault(x => x.ParticleType == type);
        }

        public void Play(Transform target,Action callback=null)
        {
            Anchor anchor = new Anchor()
            {
                Scale = Vector3.one,
                EulerAngles = Vector3.zero,
                Position = Vector3.zero
            };
            
            if (target.TryGetComponent(out WorldSpacePanelOffset worldSpacePanelOffset))
                anchor = worldSpacePanelOffset.Anchor;

            var particlePosition = transform;

            particlePosition.position = target.position + anchor.Position;
            particlePosition.eulerAngles = target.eulerAngles + anchor.EulerAngles;
            particlePosition.localScale = anchor.Scale;
            
            _activeConfig.Particle.Play();
            DOVirtual.DelayedCall(5f, () =>
            {
                callback?.Invoke();
            });
        }

        public override void Reset()
        {
            base.Reset();
            gameObject.Deactivate();
        }
    }
}
