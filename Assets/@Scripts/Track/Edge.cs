using System;
using Track.Nodes;

namespace Track
{
    [Serializable]
    public class Edge
    {
        public Node nodeA;
        public Node nodeB;
        public uint weight;
    }
}
