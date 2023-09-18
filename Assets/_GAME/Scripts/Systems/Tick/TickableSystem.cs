using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Scripts.Systems.Tick
{
    public class TickableSystem : MonoBehaviour
    {
        private readonly List<ITickableComponent> _components = new List<ITickableComponent>();

        public void AddToList(ITickableComponent component)
        {
            _components.Add(component);
        }

        public void RemoveFromList(ITickableComponent component)
        {
            _components.Remove(component);
        }

        private void Update()
        {
            for (var i = 0; i < _components.Count; i++) _components[i].Tick(Time.deltaTime);
        }
    }
}