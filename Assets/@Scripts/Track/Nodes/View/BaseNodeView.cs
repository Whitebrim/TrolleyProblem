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
            
            sellPriceView.text = _node.SellPrice.ToString("0.##", CultureInfo.InvariantCulture) + "x";
            _node.OnSellPriceChanged += sellPrice => 
                sellPriceView.text = sellPrice.ToString("0.##", CultureInfo.InvariantCulture) + "x";
        }

        private void OnDisable()
        {
            sellPriceView.text = "";
        }
    }
}
