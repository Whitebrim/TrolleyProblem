using System.Globalization;
using TMPro;
using UnityEngine;

namespace Track.Nodes.View
{
    [RequireComponent(typeof(BaseNode))]
    public class BaseNodeView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro sellPriceView;
        private BaseNode _node;
        
        private void Start()
        {
            _node = GetComponent<BaseNode>();
            
            UpdateSellPriceView(_node.SellPrice);
            _node.OnSellPriceChanged += UpdateSellPriceView;
        }
        
        private void OnDisable()
        {
            _node.OnSellPriceChanged -= UpdateSellPriceView;
            sellPriceView.text = "";
        }
        
        private void UpdateSellPriceView(float sellPrice)
        {
            sellPriceView.text = sellPrice.ToString("0.##", CultureInfo.InvariantCulture) + "x";
        }
    }
}
