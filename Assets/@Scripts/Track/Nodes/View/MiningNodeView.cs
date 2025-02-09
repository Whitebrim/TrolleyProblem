using System.Globalization;
using TMPro;
using UnityEngine;

namespace Track.Nodes.View
{
    [RequireComponent(typeof(MiningNode))]
    public class MiningNodeView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro miningModificatorView;
        private MiningNode _node;
    
        private void Start()
        {
            _node = GetComponent<MiningNode>();
            
            miningModificatorView.text = _node.MiningModificator.ToString("0.##", CultureInfo.InvariantCulture) + "x";
            _node.OnMiningModificatorChanged += miningModificator => 
                miningModificatorView.text = miningModificator.ToString("0.##", CultureInfo.InvariantCulture) + "x";
        }
        
        private void OnDisable()
        {
            miningModificatorView.text = "";
        }
    }
}
