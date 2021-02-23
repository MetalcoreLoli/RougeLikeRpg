using System;

namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public class TextDependencyProperty : DependencyProperty
    {
        public TextDependencyProperty(string value)
        {
            Value = value;
        }

        public Int32 Length => ((string)Value).Length;

        public static implicit operator TextDependencyProperty(string value)
            => new TextDependencyProperty(value);

        public override string ToString()
            => (string)Value;
    }
}