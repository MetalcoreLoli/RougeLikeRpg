using System;


namespace RougeLikeRPG.Engine.Dices
{
    internal interface IDice
    {
        Edge[] Edges { get; set; }
        Int32 CountOfEdges { get; set; }
    }
}
