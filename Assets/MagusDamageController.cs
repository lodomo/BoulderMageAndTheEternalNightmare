using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagusDamageController : MonoBehaviour
{
    [SerializeField] private HandHealth handHealth;
    private void OnTriggerEnter2D(Collider2D other)
    {
        handHealth.FakeTriggerEnter(other);
    }
}
