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

            UpdateNodeNameView(_node.NodeName);
            _node.OnNodeNameChanged += UpdateNodeNameView;
        }
        
        private void OnDisable()
        {
            _node.OnNodeNameChanged -= UpdateNodeNameView;
            nameView.text = "";
        }
        
        private void UpdateNodeNameView(string nodeName)
        {
            nameView.text = nodeName;
        }
    }
}
