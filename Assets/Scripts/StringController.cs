using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringController : MonoBehaviour
{
    public Transform pointA; // Điểm đầu
    public Transform pointB; // Điểm cuối
    private LineRenderer lineRenderer;

    void Start()
    {
        // Lấy hoặc thêm LineRenderer
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Cấu hình LineRenderer
        lineRenderer.transform.position = new Vector3(0, 0, 0);
        lineRenderer.positionCount = 2; // 2 điểm: đầu và cuối
        lineRenderer.startWidth = 0.05f; // Độ dày dây (đầu)
        lineRenderer.endWidth = 0.05f;   // Độ dày dây (cuối)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Shader cho 2D
    }

    void Update()
    {
        // Vẽ dây nối giữa pointA và pointB
        if (pointA != null && pointB != null)
        {
            lineRenderer.SetPosition(0, pointA.position); // Điểm đầu
            lineRenderer.SetPosition(1, pointB.position); // Điểm cuối
        }
    }
}
