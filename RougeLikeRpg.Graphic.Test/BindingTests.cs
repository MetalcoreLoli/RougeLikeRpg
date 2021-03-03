using System;
using NUnit.Framework;
using RougeLikeRpg.Graphic.Controls.Binding;

namespace RougeLikeRpg.Graphic.Test
{
    internal class ViewModel: INotifyPropertyChanged
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public event Action<object, string> PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, prop);
        }
    }

    public class BindingTests
    {
        private DependencyProperty _property;
        private DataContext _context;
        private ViewModel _mock;
        private Binder _binder;
        
        [SetUp]
        public void Setup()
        {
            _mock = new ViewModel();
            _property = new TextDependencyProperty("");
            _context = new DataContext(_mock);
            _binder = new Binder(_context);
        }

        [Test]
        public void BindTest()
        {
            _binder.Bind(_property, "Title");
            _mock.Title = "test";
           
            CreateBinding<, ViewModel>
            Assert.AreEqual(_property.Value.ToString(), _mock.Title);
        }

        [Test]
        public void UnbindTest()
        {
            _binder.Unbind();
            _property.Value = "test";
            _mock.Title = "test1";
            Assert.That(_property.Value.ToString() == _mock.Title, Is.False);
        }
    }
}