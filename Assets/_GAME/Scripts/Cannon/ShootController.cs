using _GAME.Scripts.Bullet;
using _GAME.Scripts.Components;
using _GAME.Scripts.Components.Camera;
using _GAME.Scripts.Particles;
using _GAME.Scripts.Pools;
using _GAME.Scripts.Systems;
using _GAME.Scripts.Systems.Tick;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _GAME.Scripts.Cannon
{
    public class ShootController : MonoBehaviour, IPoolClaimer, ITickableSystemClaimer, IComponentInitializer,
        ITickableComponent
    {
        [SerializeField] private CameraFeedback cameraFeedback;
        private PoolHandler _pool;
        private TickableSystem _tickableSystem;
        private CannonTrajectory _trajectory;
        private CannonVisualView _cannonVisualView;

        public void GetPool(PoolHandler pool)
        {
            _pool = pool;
        }

        public void ClaimTickableSystem(TickableSystem tickableSystem)
        {
            _tickableSystem = tickableSystem;
            _tickableSystem.AddToList(this);
        }

        public void Initialize()
        {
            _trajectory = GetComponentInChildren<CannonTrajectory>();
            _cannonVisualView = GetComponentInChildren<CannonVisualView>();
        }

        public void Tick(float time)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (Input.touchCount > 1)
            {
                var touch = Input.GetTouch(1);
                if (touch.phase == TouchPhase.Began)
                {
                    var bulletPool = _pool.GetPool<BulletsPool>();
                    var bullet = bulletPool.GetItem() as BulletLogic;
            
                    if (bullet != null)
                    {
                        cameraFeedback.ShakeCamera();
                        VisualShoot();
                
                        _tickableSystem.AddToList(bullet);
                        var power = _trajectory.Power;
                        var direction = _trajectory.GetTrajectoryPositions();
                        bullet.Shoot(direction, power);
                
                        bullet.OnDie += OnDieEvent;
                        bullet.OnRicochet += OnRicochetEvent;
                    }
                }
               
            }
        }

        private void OnRicochetEvent(BulletLogic bullet)
        {
            bullet.OnRicochet -= OnRicochetEvent;

            var particlePool = _pool.GetPool<ParticlesPool>() as ParticlesPool;
            if (particlePool != null)
            {
                var particle = particlePool.GetItem(ParticleType.Decal);
                particle.Play(bullet.transform, () => { particlePool.DeSpawn(particle); });
            }
            
        }

        private void VisualShoot()
        {
            _cannonVisualView.PlayShoot();
            var particlePool = _pool.GetPool<ParticlesPool>() as ParticlesPool;
            if (particlePool != null)
            {
                var particle = particlePool.GetItem(ParticleType.CannonShoot);
                var shootPoint = _cannonVisualView.GetShootPoint();
                particle.Play(shootPoint, () => { particlePool.DeSpawn(particle); });
            }
        }

        private void OnDieEvent(BulletLogic bullet)
        {
            bullet.OnDie -= OnDieEvent;
            
            _tickableSystem.RemoveFromList(bullet);
            var particlePool = _pool.GetPool<ParticlesPool>() as ParticlesPool; 
            if (particlePool != null)
            {
                var particle = particlePool.GetItem(ParticleType.HitFX);
                particle.Play(bullet.transform, () => { particlePool.DeSpawn(particle); });
            }
            
            var bulletPool = _pool.GetPool<BulletsPool>();
            bulletPool.DeSpawn(bullet);
        }
    }
}