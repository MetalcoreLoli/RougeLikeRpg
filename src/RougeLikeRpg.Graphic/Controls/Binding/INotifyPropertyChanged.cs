using System;

namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public interface INotifyPropertyChanged
    {
        event Action<object, string> PropertyChanged;
    }
}