using System.Collections.Generic;
using System.Linq;
using _GAME.Scripts.Ui.Base;

namespace _GAME.Scripts.UI.Screen
{
    public class ScreenSpace : BaseUIView
    {
        private List<BaseWindow> _windows = new List<BaseWindow>();

        public void Init()
        {
            _windows = GetComponentsInChildren<BaseWindow>(true).ToList();

            for (var i = 0; i < _windows.Count; i++) _windows[i].Init();

        }

        public BaseWindow GetWindow<T>()
        {
            var window = _windows.Find(w => w.GetType() == typeof(T));
            return window;
        }
        public List<BaseWindow> GetWindows<T>()
        {
            var baseWindows = _windows.FindAll(w => w.GetType() == typeof(T));
            return baseWindows;
        }
    }

}