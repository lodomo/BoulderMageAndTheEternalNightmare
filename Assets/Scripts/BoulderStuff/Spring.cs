using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float springForce;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var victim = other.gameObject.GetComponent<Rigidbody2D>();
        var temp = victim.velocity;
        temp.y = 0;
        victim.velocity = temp;
        victim.AddForce(Vector2.left * springForce, ForceMode2D.Impulse);
    }
}
