using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyOtherCheck : MonoBehaviour
{
    [SerializeField] private bool touchingOther;
    public bool TouchingOther => touchingOther;
    private Transform _transform;
    const float GroundedRadius = .2f;
    [SerializeField] private GameObject parent;
    private void Awake()
    {
        _transform = transform;
        parent = _transform.parent.gameObject;
    }

    private void Update()
    {
        var touchingAnything = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, GroundedRadius);
        foreach (var collider2D in colliders)
        {
            var other = collider2D.gameObject;
            
            if (other != parent)
            {
                touchingAnything = true;
            }
        }

        touchingOther = touchingAnything;
    }
}
