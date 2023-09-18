using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Scripts.Trajectory
{
    public class TrajectoryCalculation : MonoBehaviour
    {
        private static readonly float PredictionTime = 2.2f;
        private static readonly float MaxPower = 100;
        private static readonly int MaxPoints = 100;
        
        public static Vector3[] PredictTrajectory(float power, Transform holder,LayerMask mask)
        {
            var positions = new List<Vector3>();
            positions.Add(holder.position);

            var eulerAngles = holder.eulerAngles;
            var xRotation = eulerAngles.x;
            var rotation = Quaternion.Euler(xRotation, eulerAngles.y, eulerAngles.z);
            var forwardDirection = rotation * Vector3.forward;

            var maxVelocity = power * 10.0f;

            var initialVelocity = CalculateInitialVelocity(forwardDirection, maxVelocity,power);

            for (var i = 1; i < MaxPoints; i++)
            {
                var time = i * (PredictionTime / MaxPoints);
                
                var newPosition = positions[0] + initialVelocity * time + Physics.gravity * (0.5f * Mathf.Pow(time, 2));

                var ray = new Ray(positions[i - 1], newPosition - positions[i - 1]);

                if (Physics.Raycast(ray, out _, Vector3.Distance(positions[i - 1], newPosition),mask))
                {
                    break;
                }

                positions.Add(newPosition);

                if (i == MaxPoints - 1) positions[i] = newPosition;
            }
            

            return positions.ToArray();
        }

        private static  Vector3 CalculateInitialVelocity(Vector3 direction, float maxVelocity,float power)
        {
            var velocity = Mathf.Lerp(0, maxVelocity, power / MaxPower);
            return direction * velocity;
        }

        public static float GetMaxPower()
        {
            return MaxPower;
        }
    }
}
