using System.Collections.Generic;
using System.Linq;
using _GAME.Scripts.Base;
using _GAME.Scripts.Bullet;
using _GAME.Scripts.Tools;
using UnityEngine;

namespace _GAME.Scripts.Pools
{
    public class BulletsPool : Pool
    {
        protected override void Init(int itemsCount)
        {
            base.Init(itemsCount);
            var items = listOfItems.ConvertAll(itm => (BulletLogic)itm);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].StartSetup(transform);
            }
        }

        public override BaseView GetItem()
        {
            var items = listOfItems.ConvertAll(itm => (BulletLogic)itm);
            var item = items.FirstOrDefault(x => !x.isActiveAndEnabled);
            
            if (item!=null)
            {
                item.SetupItemView();
            }
            else
            {
                SpawnSpecial(100);
                Debug.Log("SPAWNED MORE 100 ");
                items = listOfItems.ConvertAll(itm => (BulletLogic)itm);
                item = items.FirstOrDefault(x => !x.isActiveAndEnabled);
            }

            listOfItems.Remove(item);
            item.Activate();
            return item;
        }

        
        private void SpawnSpecial(int count)
        {
            var newItems = new List<BaseView>();
            for (var i = 0; i < count; i++)
            {
                var item = Instantiate(baseView, transform);
                item.Init();
                item.Deactivate();
                listOfItems.Add(item);
                newItems.Add(item);
            }
            var items = newItems.ConvertAll(itm => (BulletLogic)itm);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetupItemView();
                items[i].StartSetup(transform);
            }
        }
    }
}
