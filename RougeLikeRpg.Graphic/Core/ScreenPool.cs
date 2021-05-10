using System;
using System.Collections.ObjectModel;
using RougeLikeRpg.Graphic.Controls;

namespace RougeLikeRpg.Graphic.Core
{
    public class ScreenPool
    {
        public ObservableCollection<Screen> Screens { get; set; }

        public ScreenPool()
        {
            Screens = new ObservableCollection<Screen>();
        }

        public ScreenPool Add<TScreen>(IControlConfiguration screenConfiguration) where TScreen : Screen, new()
        {
            var screen = Activator.CreateInstance(typeof(TScreen), screenConfiguration) as TScreen; 
            Screens.Add(screen);
            return this;
        }

        public ScreenPool AddExisting(Screen screen)
        {
            screen.Add(screen);
            return this;
        }
    }
}