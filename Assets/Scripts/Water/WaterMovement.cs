using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private WaterCollect _waterCollect;
    [SerializeField] private BoxCollider2D _wallCheck;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private bool isMoving = false;

    // Start is called before the first frame update
    private void Start()
    {
        _transform = transform;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isMoving) return;
        _rigidbody2D.velocity = new Vector2(_transform.localScale.x * 2, _rigidbody2D.velocity.y);
        isMoving = true;
    }
}
