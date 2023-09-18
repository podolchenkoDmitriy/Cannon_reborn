using _GAME.Scripts.Systems.Tick;

namespace _GAME.Scripts.Systems
{
    public interface ITickableSystemClaimer
    {
        public void ClaimTickableSystem(TickableSystem tickableSystem);
    }
}
