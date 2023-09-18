using UnityEngine.Events;

namespace _GAME.Scripts.Systems
{
    public interface ITickableComponent
    {
        public void Tick(float time);
    }
}
