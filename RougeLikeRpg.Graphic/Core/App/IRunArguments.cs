using System.Collections.Generic;

namespace RougeLikeRpg.Graphic.Core
{
    public interface IRunArguments
    {
        public  IEnumerable<object> Args { get; }
    }
}