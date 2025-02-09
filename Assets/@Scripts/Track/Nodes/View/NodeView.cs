using TMPro;
using UnityEngine;

namespace Track.Nodes.View
{
    [RequireComponent(typeof(Node))]
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro nameView;
        private Node _node;
    
        private void Start()
        {
            _node = GetComponent<Node>();
            
            nameView.text = _node.NodeName;
            _node.OnNodeNameChanged += nodeName => nameView.text = nodeName;
        }

        private void OnDisable()
        {
            nameView.text = "";
        }
    }
}
