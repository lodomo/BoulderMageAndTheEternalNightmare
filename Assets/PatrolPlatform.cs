using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class PatrolPlatform : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    [SerializeField] private EnemyOtherCheck groundCheck;
    [SerializeField] private EnemyOtherCheck wallCheck;
    private Vector2 _direction = new Vector2(0, 0);
    private float speed = 2;
    private Enemy _enemy;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _transform = transform;
        _enemy = gameObject.GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        Patrol();
        AngryPatrol();
    }

    private void NotOnGround()
    {
        if (groundCheck.TouchingOther) return;
        Flip();
    }
    
    private void TouchWall()
    {
        if (!wallCheck.TouchingOther) return;
        Flip();
    }

    private void Flip()
    {
        var temp = _transform.localScale;
        temp.x *= -1;
        _transform.localScale = temp;
    }

    private void Patrol()
    {
        if (_enemy.Health == 0) return;
        _direction.x = _transform.localScale.x;
        _rigidbody2D.velocity = _direction * speed;
        NotOnGround();
        TouchWall();
    }

    private void AngryPatrol()
    {
        if (!_enemy.IsAngry) return;
        _direction.x = _transform.localScale.x;
        _rigidbody2D.velocity = _direction * (speed * 5);
        NotOnGround();
        TouchWall();
    }
}
