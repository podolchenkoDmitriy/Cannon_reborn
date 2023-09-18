using _GAME.Scripts.Base;
using _GAME.Scripts.Systems;
using _GAME.Scripts.Tools;
using _GAME.Scripts.Trajectory;
using UnityEngine;
using UnityEngine.Events;

namespace _GAME.Scripts.Bullet
{
   public class BulletLogic : BaseView,ITickableComponent
   {
      public UnityAction<BulletLogic> OnDie;
      public UnityAction<BulletLogic> OnRicochet;
      
      [SerializeField] private LayerMask mask;
      private readonly float _startSpeed = 10.0f;
      private float _currentSpeed = 10.0f;
      private int _currentPositionIndex;
      private float _power;
      private float _ricochetTimes = 2;
      
      private Vector3[] _positions;
      private BulletView _bulletView;
      private Transform _startParent;

      public override void Init()
      {
         base.Init();
         _bulletView = GetComponentInChildren<BulletView>();
         _bulletView.Init();
      }
      public void StartSetup(Transform parent)
      {
         _startParent = parent;
         Transform transform1;
         (transform1 = transform).SetParent(parent);
         transform1.localPosition = Vector3.zero;
         transform1.rotation = parent.rotation;
         gameObject.Deactivate();
      }
      public void SetupItemView()
      {
         _bulletView.SetupItemView();
      }
      public override void Reset()
      {
         Transform transform1;
         (transform1 = transform).SetParent(_startParent);
         transform1.localPosition = Vector3.zero;
         transform1.rotation = _startParent.rotation;
         gameObject.Deactivate();
         base.Reset();
         _ricochetTimes = 2;
         _power = 0;
         _currentPositionIndex = 0;
      }

      public void Shoot(Vector3[] direction, float power)
      {
         _positions = new Vector3[direction.Length];
         _currentSpeed = _startSpeed * power;
         _power = power;
         _positions = direction;
      }
      public void Tick(float time)
      {
         Move();
      }
      private void Move()
      {
         if (_currentPositionIndex < _positions.Length - 1)
         {
            var startPosition = _positions[_currentPositionIndex];
            var endPosition = _positions[_currentPositionIndex + 1];
            var distance = Vector3.Distance(startPosition, endPosition);
            var timeToReachPoint = distance / _currentSpeed;
            transform.LookAt(endPosition);
            transform.position = Vector3.Lerp(startPosition, endPosition, timeToReachPoint / 2f);
            var elapsedTime = Time.deltaTime;
            var remainingTime = timeToReachPoint - elapsedTime;

            if (remainingTime <= 0) _currentPositionIndex++;
         }
         else
         {

            Ricochet();
         } 
      }
      private void Ricochet()
      {
         _ricochetTimes--;
         if (_ricochetTimes<=0)
         {
            OnDie?.Invoke(this);
            return;
         }
         OnRicochet?.Invoke(this);
         Ray ray = new Ray(transform.position, transform.forward); 
         
         if (Physics.Raycast(ray, out RaycastHit hit,Time.deltaTime*_currentSpeed + 1f,mask))
         {
            Vector3 reflect = Vector3.Reflect(ray.direction, hit.normal);
            transform.LookAt(transform.position + reflect);
            _power *= 0.15f;
            var poses = TrajectoryCalculation.PredictTrajectory(_power, transform, mask);
            Shoot(poses, _power);
            _currentPositionIndex = 0;
         }
      }
   }
}
