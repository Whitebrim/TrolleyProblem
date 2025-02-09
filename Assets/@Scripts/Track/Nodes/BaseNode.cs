using System;
using UnityEngine;

namespace Track.Nodes
{
    [Serializable]
    public class BaseNode : Node
    {
        public event Action<float> OnSellPriceChanged;
        [SerializeField] private float sellPrice;
        public float SellPrice
        {
            get => sellPrice;
            private set
            {
                if (Mathf.Approximately(sellPrice, value)) return;
                sellPrice = value;
                OnSellPriceChanged?.Invoke(value);
            }
        }
        
        private void OnValidate()
        {
            OnSellPriceChanged?.Invoke(sellPrice);
        }
    }
}