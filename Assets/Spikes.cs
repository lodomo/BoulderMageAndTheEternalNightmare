using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        print("Spike Collision Detected");
        var damagableObject = other.gameObject.GetComponent<IDamagable>();
        damagableObject.TakeDamage(1);
    }
}
