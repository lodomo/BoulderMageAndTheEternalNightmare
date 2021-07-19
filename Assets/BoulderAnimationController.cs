using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private WallCheck _wallCheck;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("xVelocity", _rigidbody2D.velocity.x);
        _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        _animator.SetBool("onGround", _groundCheck.OnGroundBool);
        _animator.SetBool("onWall", _wallCheck.onWallBool);
    }
}
