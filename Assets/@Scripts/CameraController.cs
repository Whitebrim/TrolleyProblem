using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    [Header("Mouse Drag Settings")]
    public bool enableDrag = true;

    private Vector3 _dragOrigin;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
        if (enableDrag) HandleMouseDrag();
    }

    private void HandleMovement()
    {
        var h = Input.GetAxis("Horizontal"); // A, D
        var v = Input.GetAxis("Vertical");   // W, S
        
        var forward = new Vector3(transform.up.x, 0, transform.up.z).normalized;
        var right = new Vector3(transform.right.x, 0, transform.right.z).normalized;

        var move = (forward * v + right * h) * (moveSpeed * Time.deltaTime);
        transform.position += move;
    }

    private void HandleZoom()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0) return;
        var newSize = _camera.fieldOfView - scroll * zoomSpeed;
        _camera.fieldOfView = Mathf.Clamp(newSize, minZoom, maxZoom);
    }

    private void HandleMouseDrag()
    {
        if (!Input.GetMouseButton(0)) return; 

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (!plane.Raycast(ray, out float enter)) return;
        
        var hitPoint = ray.GetPoint(enter);
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = hitPoint;
        }
        var delta = _dragOrigin - hitPoint;
        transform.position += delta;
    }

}
