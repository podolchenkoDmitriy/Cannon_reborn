using System;
using UnityEngine;

namespace _GAME.Scripts.Particles
{
    public enum ParticleType
    {
        CannonShoot,
        HitFX,
        Decal
    }
    [Serializable]
    public class ParticleConfig
    {
        public ParticleType ParticleType;
        public ParticleSystem Particle;
    }
}