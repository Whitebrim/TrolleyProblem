using System;
using UnityEngine;

namespace Track.Nodes
{
    [Serializable]
    public class MiningNode : Node
    {
        public event Action<float> OnMiningModificatorChanged;
        [SerializeField] private float miningModificator;
        public float MiningModificator
        {
            get => miningModificator;
            private set
            {
                if (Mathf.Approximately(miningModificator, value)) return;
                miningModificator = value;
                OnMiningModificatorChanged?.Invoke(value);
            }
        }
        
        private void OnValidate()
        {
            OnMiningModificatorChanged?.Invoke(miningModificator);
        }
    }
}