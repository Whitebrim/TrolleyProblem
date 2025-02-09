using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Track.View
{
    [RequireComponent(typeof(RouteMap))]
    public class RouteMapView : SerializedMonoBehaviour
    {
        [SerializeField] private GameObject edgeViewPrefab;
        private Dictionary<Edge, EdgeView> _viewMap = new(); 
        private RouteMap _routeMap;

        private void OnValidate()
        {
            _routeMap = GetComponent<RouteMap>();
            
            _routeMap.OnRouteMapChanged -= UpdateRouteView;
            _routeMap.OnRouteMapChanged += UpdateRouteView;
        }

        private void OnDisable()
        {
            _routeMap.OnRouteMapChanged -= UpdateRouteView;
        }
        
        private void DeleteRouteView()
        {
            var children = new Transform[transform.childCount];
            for (var i = 0; i < transform.childCount; i++)
            {
                children[i] = transform.GetChild(i);
            }
            foreach (var child in children)
            {
                if (child.GetComponent<EdgeView>() != null)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
            
            _viewMap = new Dictionary<Edge, EdgeView>();
        }
        
        private void UpdateRouteView()
        {
            if (!Application.isPlaying || !gameObject.scene.IsValid()) return;
            
            Dictionary<Edge, EdgeView> tempViewMap = new(); 
            foreach (var edge in _routeMap.Map.Values.SelectMany(edges => edges))
            {
                if (tempViewMap.ContainsKey(edge))
                    continue;
                if (!_viewMap.ContainsKey(edge))
                {
                    tempViewMap[edge] = CreateEdgeView(edge.nodeA.transform.position, edge.nodeB.transform.position); 
                    tempViewMap[edge].UpdateEdgeView(edge.weight);
                }
                else
                {
                    UpdateEdgeView(_viewMap[edge],
                        edge.nodeA.transform.position, edge.nodeB.transform.position);
                    _viewMap[edge].UpdateEdgeView(edge.weight);

                    tempViewMap.Add(edge, _viewMap[edge]);
                }
            }

            _viewMap = tempViewMap;
        }

        private EdgeView CreateEdgeView(Vector3 start, Vector3 end)
        {
            if (edgeViewPrefab is null)
            {
                Debug.LogError("EdgeView Prefab prefab is null!");
                return null;
            }
            
            var edgeView = Instantiate(edgeViewPrefab, transform).GetComponent<EdgeView>();
            
            UpdateEdgeView(edgeView, start, end);
            
            return edgeView;
        }

        private void UpdateEdgeView(EdgeView edgeView, Vector3 start, Vector3 end)
        {
            var midPoint = (start + end) / 2;
            var direction = end - start;
            var length = direction.magnitude;
            
            var angleY = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
            
            edgeView.transform.position = new Vector3(midPoint.x, edgeView.transform.position.y, midPoint.z);
            edgeView.transform.rotation = Quaternion.Euler(0, -angleY, 0);
            edgeView.transform.localScale = new Vector3(length, edgeView.transform.localScale.y, edgeView.transform.localScale.z);
        }
    }
}
