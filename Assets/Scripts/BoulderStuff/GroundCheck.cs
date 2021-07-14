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
    
    private bool _onGroundBool;
    public bool onGroundBool => _onGroundBool;

    private void Awake()
    {
        CoyoteWaitTime = new WaitForSeconds(CoyoteTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       OnGround?.Invoke();
       _onGroundBool = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(Co_CoyoteTime());
    }

    private IEnumerator Co_CoyoteTime()
    {
        yield return CoyoteWaitTime;
        OffGround?.Invoke();
        _onGroundBool = false;
    }
}
