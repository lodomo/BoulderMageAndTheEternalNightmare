using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoorScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.position.y > transform.position.y) return;
        var otherRB2D = other.gameObject.GetComponent<Rigidbody2D>();
        otherRB2D.velocity = Vector2.up * 30;
    }
}
