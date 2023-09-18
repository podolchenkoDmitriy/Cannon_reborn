using _GAME.Scripts.Base;
using _GAME.Scripts.Systems;
using _GAME.Scripts.Systems.Tick;
using _GAME.Scripts.Trajectory;
using _GAME.Scripts.UI.Screen;
using _GAME.Scripts.UI.Windows;
using UnityEngine;

namespace _GAME.Scripts.Cannon
{
    public class CannonTrajectory : BaseView, IScreenSpaceClaimer, ITickableSystemClaimer, ITickableComponent
    {
        [SerializeField] private LineRenderer trajectory;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform cannonProjectileHolder;
        [SerializeField] private float powerDecrease;

        public float Power { get; private set; }
        
        private Vector3[] _trajectoryPositions;

        public void ClaimScreenSpaceCanvas(ScreenSpace screenSpace)
        {
            var w = screenSpace.GetWindow<GameWindow>() as GameWindow;
            if (w != null)
            {
                w.OnChangeValue += ChangePower;
                Power = 0.5f;
                w.SimulatePower(Power);
            }
        }

        public void ClaimTickableSystem(TickableSystem tickableSystem)
        {
            tickableSystem.AddToList(this);
        }

        private void ChangePower(float value)
        {
            Power = TrajectoryCalculation.GetMaxPower() * value;
        }

        public void Tick(float time)
        {
            var power = Power * powerDecrease;
            _trajectoryPositions = TrajectoryCalculation.PredictTrajectory(power, cannonProjectileHolder, layerMask);
            trajectory.positionCount = _trajectoryPositions.Length;
            trajectory.SetPositions(_trajectoryPositions);
        }

        public Vector3[] GetTrajectoryPositions()
        {
            return _trajectoryPositions;
        }
    }
}