using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Action OnGround;
    public Action OffGround;

    [SerializeField] private float CoyoteTime;
    private WaitForSeconds CoyoteWaitTime;
    //private bool reGround = false;
    
    private bool _onGroundBool;
    public bool OnGroundBool => _onGroundBool;
    private bool onGroundThisFrame;

    private Transform _transform;
    const float GroundedRadius = .2f;
    [SerializeField] private GameObject boulderMage;

    private void Awake()
    {
        CoyoteWaitTime = new WaitForSeconds(CoyoteTime);
        _transform = transform;
    }

    private void FixedUpdate()
    {
        var lastFrame = onGroundThisFrame;
        onGroundThisFrame = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != boulderMage)
            {
                onGroundThisFrame = true;
            }
        }

        if (lastFrame && onGroundThisFrame) return;

        if (onGroundThisFrame)
        {
            _onGroundBool = true;
            OnGround?.Invoke();
        }
        else
        {
            _onGroundBool = false;
            OffGround?.Invoke();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (_onGroundBool)
    //     {
    //         reGround = true;
    //     }
    //     OnGround?.Invoke();
    //    _onGroundBool = true;
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     StartCoroutine(Co_CoyoteTime());
    // }

    // private IEnumerator Co_CoyoteTime()
    // {
    //     yield return CoyoteWaitTime;
    //     
    //     if (!reGround)
    //     {
    //         OffGround?.Invoke();
    //         _onGroundBool = false;
    //     }
    //     
    //     reGround = false;
    //
    // }
}
