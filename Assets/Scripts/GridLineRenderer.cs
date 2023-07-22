using UnityEngine;

public class GridLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Color lineColor = Color.white;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Start()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Cell"))
            {
                StartDrawingLine(hit.collider.gameObject.transform.position);
            }
        }

        if (Input.GetMouseButton(0))
        {
            UpdateLineEndPoint();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDrawingLine();
        }
    }

    private void StartDrawingLine(Vector3 startPoint)
    {
        this.startPoint = startPoint;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint);
    }

    private void UpdateLineEndPoint()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - startPoint;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float snapAngle = Mathf.Round(angle / 90f) * 90f;
        Vector3 snappedDirection = Quaternion.Euler(0f, 0f, snapAngle) * Vector3.right;
        endPoint = startPoint + snappedDirection.normalized * Vector3.Distance(startPoint, mousePosition);
        lineRenderer.SetPosition(1, endPoint);
    }

    private void EndDrawingLine()
    {
        if (endPoint != startPoint)
        {
            RaycastHit2D hit = Physics2D.Raycast(endPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Cell"))
            {
                endPoint = hit.collider.gameObject.transform.position;
            }
        }

        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.positionCount = 0;
    }
}
