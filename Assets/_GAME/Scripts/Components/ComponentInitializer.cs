using System.Linq;
using _GAME.Scripts.Pools;
using _GAME.Scripts.Systems;
using _GAME.Scripts.Systems.Tick;
using _GAME.Scripts.UI.Screen;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Components
{
    public class ComponentInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject Root;
        [SerializeField] private ScreenSpace _screenSpaceCanvas;
        [SerializeField] private PoolHandler _poolHandler;
        [SerializeField] private TickableSystem _tickableSystem;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            DOTween.SetTweensCapacity(625,250);
            
            _screenSpaceCanvas.Init();
            _poolHandler.Initialize();
            
            InitTickableComponents();
            InitializeCanvasClaimers();
            
            InitPoolHandlers();

            InitializeComponents();
        }

        private void InitTickableComponents()
        {
            var claimers = Root.GetComponentsInChildren<ITickableSystemClaimer>().ToList();
            for (int i = 0; i < claimers.Count; i++)
            {
                claimers[i].ClaimTickableSystem(_tickableSystem);
            }
        }


        private void InitializeCanvasClaimers()
        {
            var screenSpaces = Root.GetComponentsInChildren<IScreenSpaceClaimer>(true);
            for (var i = 0; i < screenSpaces.Length; i++) screenSpaces[i].ClaimScreenSpaceCanvas(_screenSpaceCanvas);
        }

        private void InitializeComponents()
        {
            var components = Root.GetComponentsInChildren<IComponentInitializer>(true).ToList();
            for (var i = 0; i < components.Count; i++) components[i].Initialize();
        }

        private void InitPoolHandlers()
        {
            var poolClaimer = Root.GetComponentsInChildren<IPoolClaimer>(true);
            for (var i = 0; i < poolClaimer.Length; i++) poolClaimer[i].GetPool(_poolHandler);
        }

    }
}