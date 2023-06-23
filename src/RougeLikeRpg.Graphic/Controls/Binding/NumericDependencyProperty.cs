namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public class NumericDependencyProperty : DependencyProperty
    {
        private int _num;
        public NumericDependencyProperty(int num)
        {
            Value = num;
        }
        
        public  static implicit operator NumericDependencyProperty(int num)
            => new(num);

        public static implicit operator int(NumericDependencyProperty property)
            => (int)property.Value;
}
}