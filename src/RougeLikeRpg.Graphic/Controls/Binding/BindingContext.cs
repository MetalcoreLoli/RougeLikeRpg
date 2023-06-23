
using System;
using System.Reflection;

namespace RougeLikeRpg.Graphic.Controls.Binding
{
    public class DataContext
    {
        private readonly object _graph;

        public DataContext(object graph)
        {
            this._graph = graph;
        }

        public Type GetTypeOfGraph() => _graph.GetType();
        
        public PropertyInfo GetPropertyOfGraph(string name) => GetTypeOfGraph().GetProperty(name);

        public object Graph => _graph;
    }
}