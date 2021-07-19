using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    [SerializeField] private bool isWarpable;
    [SerializeField] private GameObject pickupCoinEffect;
    private Transform _transform;
    private Transform boulderMage;
    private Rigidbody2D rb2d;

    void Awake()
    {
        _transform = transform;
        boulderMage = GameObject.Find("BoulderMage").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        WarpCoin();
    }

    private void WarpCoin()
    {
        if (!isWarpable) return;
        StartCoroutine(Co_WarpCoin());
    }

    private IEnumerator Co_WarpCoin()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            var dom = boulderMage.position;
            var sub = _transform.position;
            
            var direction = (dom - sub).normalized;
            var xDifference = Math.Abs(dom.x - sub.x);
            var yDifference = Math.Abs(dom.y - sub.y);

            var moveSpeed = Math.Max(xDifference, yDifference) * 8;

            if (moveSpeed < 5)
            {
                moveSpeed = 5;
            }
            rb2d.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
            
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherInterface = other.gameObject.GetComponent<ICanGetCoins>();
        if (otherInterface != null)
        {
            otherInterface.GetCoin();
            Instantiate(pickupCoinEffect, _transform.position, _transform.rotation);
            Destroy(gameObject);
        }
    }
}
