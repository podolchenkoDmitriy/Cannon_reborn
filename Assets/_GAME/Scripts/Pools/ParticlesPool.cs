using System.Linq;
using _GAME.Scripts.Particles;
using _GAME.Scripts.Tools;

namespace _GAME.Scripts.Pools
{
    public class ParticlesPool : Pool
    {
        public ParticlesView GetItem(ParticleType type)
        {
            var items = listOfItems.ConvertAll(itm => (ParticlesView)itm);
            var item = items.FirstOrDefault(x => !x.isActiveAndEnabled);

            if (item != null) item.SetupItemView(type);
            listOfItems.Remove(item);
            item.Activate();
            return item;
        }
    }
}