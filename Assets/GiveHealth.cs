using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveHealth : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Heal Collision Detected");
        var healable = other.gameObject.GetComponent<IHealable>();
        healable?.GiveHealth(1);
        StartCoroutine(DestroyNextFrame());
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
