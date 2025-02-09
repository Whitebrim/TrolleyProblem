using TMPro;
using UnityEngine;

namespace Track.View
{
    public class EdgeView : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI weightView;
        
        public void UpdateEdgeView(uint weight)
        {
            canvas.transform.localScale = new Vector3(
                1 / transform.localScale.x,
                1 / transform.localScale.z,
                canvas.transform.localScale.z);
            weightView.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            weightView.text = weight.ToString();
        }
    }
}
