﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovementController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển
    public Vector2 leftLimit;   // Tọa độ giới hạn bên trái (x, y)
    public Vector2 rightLimit;  // Tọa độ giới hạn bên phải (x, y)

    private bool movingRight = false; // Đặt false để di chuyển bên trái trước
    private SpriteRenderer spriteRenderer; // SpriteRenderer để flip sprite

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveRat();
    }

    private void MoveRat()
    {
        // Di chuyển chuột dựa trên hướng hiện tại
        float moveDirection = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);

        // Kiểm tra nếu đạt giới hạn bên phải
        if (movingRight && transform.position.x >= rightLimit.x)
        {
            TurnAround(false); // Quay đầu sang trái
        }
        // Kiểm tra nếu đạt giới hạn bên trái
        else if (!movingRight && transform.position.x <= leftLimit.x)
        {
            TurnAround(true); // Quay đầu sang phải
        }
    }

    private void TurnAround(bool toRight)
    {
        movingRight = toRight;

        // Flip sprite bằng cách thay đổi flipX
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = toRight;
        }
        else
        {
            // Hoặc lật bằng cách thay đổi localScale.x
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (toRight ? 1 : -1);
            transform.localScale = scale;
        }
    }
}