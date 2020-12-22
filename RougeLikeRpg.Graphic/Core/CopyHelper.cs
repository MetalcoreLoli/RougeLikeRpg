using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace RougeLikeRpg.Graphic.Core
{
    public static class CopyHelper
    {
        public static T DeepCopy<T>(this T graph) where T : class 
        {
            using var mem = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(mem, graph);
            mem.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(mem) as T;
        }
    }
}
