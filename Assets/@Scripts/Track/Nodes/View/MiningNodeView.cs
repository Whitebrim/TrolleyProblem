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

            UpdateMiningModificatorView(_node.MiningModificator);
            _node.OnMiningModificatorChanged += UpdateMiningModificatorView;
        }

        private void UpdateMiningModificatorView(float miningModificator)
        {
            miningModificatorView.text = miningModificator.ToString("0.##", CultureInfo.InvariantCulture) + "x";
        }

        private void OnDisable()
        {
            _node.OnMiningModificatorChanged -= UpdateMiningModificatorView;
            miningModificatorView.text = "";
        }
    }
}
