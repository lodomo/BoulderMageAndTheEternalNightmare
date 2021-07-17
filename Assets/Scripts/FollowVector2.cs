using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVector2 : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Transform Dom;
    private Vector2 dom;
    private Vector2 sub;
    private float moveSpeed;
    private const float MaxDistance = 4;
    private const float ProposedMoveSpeed = 30;
    [SerializeField] public bool isFollowing = false;
    private Transform _transform;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    private void Awake()
    {
        _transform = transform;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (!isFollowing) return;
        StartFollow();
    }

    private IEnumerator FollowTheLeader()
    {
        isFollowing = true;
        yield return _waitForFixedUpdate;
        
        while (isFollowing)
        {
            if (TooFarFromLeader(dom, sub))
            {
                _transform.position = dom;
            }
            
            dom = Dom.position;
            sub = _transform.position;
            
            var direction = (dom - sub).normalized;
            var xDifference = Math.Abs(dom.x - sub.x);
            var yDifference = Math.Abs(dom.y - sub.y);

            moveSpeed = Math.Max(xDifference, yDifference) * ProposedMoveSpeed;

            if (moveSpeed > 300)
            {
                moveSpeed = 300;
            }
            rb2d.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            
            yield return _waitForFixedUpdate;
        }
    }

    public void StartFollow()
    {
        StartCoroutine(FollowTheLeader());
    }

    private bool TooFarFromLeader(Vector2 dom, Vector2 sub)
    {
        var dif = dom - sub;
        dif.x = Math.Abs(dif.x);
        dif.y = Math.Abs(dif.y);

        if (dif.x > MaxDistance || dif.y > MaxDistance)
        {
            return true;
        }

        return false;
    }
    
    
}
