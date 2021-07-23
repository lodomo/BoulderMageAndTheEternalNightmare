using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public Action OnWall;
    public Action OffWall;

    [SerializeField] private bool _onWallBool;
    public bool onWallBool => _onWallBool;
    
    //private bool reWall = false;
    
    private Transform _transform;
    const float GroundedRadius = .5f;
    [SerializeField] private GameObject boulderMage;
    private bool onWallThisFrame = false;

    private void Awake()
    {
        _transform = transform;
    }
    
    private void FixedUpdate()
    {
        var lastFrame = onWallThisFrame;
        onWallThisFrame = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != boulderMage)
            {
                var moonTag = colliders[i].gameObject.GetComponent<MoonTags>();
                if (moonTag != null && moonTag.TagList == TagList.Stage)
                {
                    onWallThisFrame = true;
                }
            }
        }
        
        if (lastFrame && onWallThisFrame) return;

        if (onWallThisFrame)
        {
            _onWallBool = true;
            OnWall?.Invoke();
        }
        else
        {
            _onWallBool = false;
            OffWall?.Invoke();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (_onWallBool)
    //     {
    //         reWall = true;
    //     }
    //     
    //     var otherTag = other.gameObject.GetComponent<MoonTags>();
    //     if (otherTag == null) return;
    //     if (otherTag.TagList != TagList.Stage) return;
    //     OnWall?.Invoke();
    //     _onWallBool = true;
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     var otherTag = other.gameObject.GetComponent<MoonTags>();
    //     if (otherTag == null) return;
    //     if (otherTag.TagList != TagList.Stage) return;
    //     OffWall?.Invoke();
    //     
    //     if (reWall)
    //     {
    //         reWall = !reWall;
    //     }
    //     else
    //     {
    //         _onWallBool = false;
    //     }
    //     
    // }
}
