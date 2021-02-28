namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public class Binder
    {
        private readonly DataContext _context;
        private DependencyProperty _property;
        public Binder(DataContext context)
        {
            _context = context;
        }

        public void Bind(DependencyProperty property, string propertyName)
        {
            _property = property;
            var prop = _context.GetPropertyOfGraph(propertyName);

            if (_context.Graph is INotifyPropertyChanged propertyChanged)
            {
                property.Value = prop.GetValue(_context.Graph);
                propertyChanged.PropertyChanged += OnUpdateValue;
            }
        }

        public void Unbind()
        {   
            if (_context.Graph is INotifyPropertyChanged propertyChanged)
                propertyChanged.PropertyChanged -= OnUpdateValue;
            _property = null;
        }

        private void OnUpdateValue(object sender, string propName)
        {
            var prop = _context.GetPropertyOfGraph(propName);
            _property.Value = prop.GetValue(_context.Graph);
        }
    }
}