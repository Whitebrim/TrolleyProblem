using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Track.Nodes;
using UnityEngine;

namespace Track
{
    public class RouteMap : SerializedMonoBehaviour
    {
        public event Action OnRouteMapChanged;
        [field: SerializeField] public Dictionary<Node, List<Edge>> Map { get; } = new();

        private void OnValidate()
        {
            foreach (var kvp in Map.ToList())
            {
                for (var i = 0; i < kvp.Value.Count; i++)
                {
                    if (kvp.Value[i].nodeA is null && kvp.Value[i].nodeB is null)
                        kvp.Value[i].nodeA = kvp.Key;
                    if ((kvp.Value[i].nodeA != kvp.Key && kvp.Value[i].nodeB != kvp.Key) || 
                        (kvp.Value[i].nodeA == kvp.Key && kvp.Value[i].nodeB == kvp.Key))
                    {
                        kvp.Value.RemoveAt(i);
                        i--;
                        continue;
                    }
                    var secondNode = kvp.Value[i].nodeA == kvp.Key ? kvp.Value[i].nodeB : kvp.Value[i].nodeA;
                    if (secondNode is null) continue;
                    if (Map.ContainsKey(secondNode))
                    {
                        if (!Map[secondNode].Contains(kvp.Value[i]))
                            Map[secondNode].Add(kvp.Value[i]);
                    }
                    else
                    {
                        Map.Add(secondNode, new List<Edge> {kvp.Value[i]});
                    }
                }
            }
            OnRouteMapChanged?.Invoke();
        }
    }
}
