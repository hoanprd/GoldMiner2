using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public Transform pivot; // Điểm quay (ông già đào vàng)
    public float radius = 2f; // Bán kính của hình bán nguyệt
    public float rotationSpeed = 50f; // Tốc độ quay
    public float minAngle = -90f; // Góc quay nhỏ nhất (độ)
    public float maxAngle = 90f;  // Góc quay lớn nhất (độ)
    public float shootSpeed = 5f; // Tốc độ bắn móc câu
    public float baseReturnSpeed = 7f; // Tốc độ quay về của móc câu (không có vàng)
    public float explosionRadius = 5f; // Bán kính nổ
    public float explosionForce = 10f; // Lực nổ (cho các đối tượng bị nổ bay ra)
    public GameObject bombExplosionFXPrefab, tntExplosionFXPrefab;
    public Animator playerAnim;

    private float currentAngle; // Góc hiện tại của móc câu
    private bool movingClockwise = true; // Hướng di chuyển
    private bool isShooting = false; // Trạng thái bắn móc câu
    private bool isReturning = false; // Trạng thái quay về
    private bool isHookTNT = false;
    private Vector3 shootDirection; // Hướng bắn của móc câu
    private Vector3 initialPosition; // Vị trí ban đầu của móc câu
    private Transform currentObject; // Vật thể đang bị kéo

    private GameObject attached = null; // Tham chiếu đến vàng được gắn (nếu có)
    private bool playPullFX;

    private void Update()
    {
        if (isReturning)
        {
            ReturnToStart();
        }
        else if (isShooting)
        {
            ShootHook();
        }
        else
        {
            UpdateRotation();
        }

        // Kiểm tra sự kiện bắn móc câu (khi game chưa kết thúc)
        if (Input.GetMouseButtonDown(0) && !isShooting && !isReturning)
        {
            /*if (IsPointerOverUI())
            {
                return;
            }*/

            StartShooting();
        }
    }

    private void UpdateRotation()
    {
        //Kiểm tra min max góc quay
        if (movingClockwise)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= maxAngle)
            {
                currentAngle = maxAngle;
                movingClockwise = false;
            }
        }
        else
        {
            currentAngle -= rotationSpeed * Time.deltaTime;
            if (currentAngle <= minAngle)
            {
                currentAngle = minAngle;
                movingClockwise = true;
            }
        }

        float radians = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Sin(radians), -Mathf.Cos(radians), 0) * radius;
        transform.position = pivot.position + offset;

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void StartShooting()
    {
        isShooting = true;
        initialPosition = transform.position; // Ghi nhận vị trí trước khi bắn
        float radians = currentAngle * Mathf.Deg2Rad;
        shootDirection = new Vector3(Mathf.Sin(radians), -Mathf.Cos(radians), 0);
    }

    private void ShootHook()
    {
        transform.position += shootDirection * shootSpeed * Time.deltaTime;
    }

    private void ReturnToStart()
    {
        float returnSpeed = baseReturnSpeed;

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
        {
            isReturning = false;
            isShooting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("hookRange"))
        {
            isReturning = true;
        }
    }
}
