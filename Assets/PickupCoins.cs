using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{
    [SerializeField] private bool isFloating;
    [SerializeField] private GameObject pickupCoinEffect;
    private Transform _transform;

    void Awake()
    {
        _transform = transform;
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
