using System;

namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public class TextDependencyProperty : DependencyProperty
    {
        public TextDependencyProperty(string value)
        {
            Value = value;
        }

        public Int32 Length => ((string) Value).Length;

        public static implicit operator TextDependencyProperty(string value)
            => new TextDependencyProperty(value);

        public static implicit operator string(TextDependencyProperty property)
            => (string) property.Value;
        
        public override string ToString()
            => (string)Value;

        public char this[int i] => ((string)Value)[i];
    }
}