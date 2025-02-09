using System;
using UnityEngine;

namespace Track.Nodes
{
    [Serializable]
    public class Node : MonoBehaviour
    {
        public event Action<string> OnNodeNameChanged;
        [SerializeField] private string nodeName;
        public string NodeName
        {
            get => nodeName;
            private set
            {
                if (nodeName == value) return;
                nodeName = value;
                OnNodeNameChanged?.Invoke(value);
            }
        }
        
        private void OnValidate()
        {
            OnNodeNameChanged?.Invoke(nodeName);
        }
    }
}
