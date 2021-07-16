using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffFaceProperly : MonoBehaviour
{
    [SerializeField] private Transform referenceDirection;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }
    private void FixedUpdate()
    {
        _transform.localScale = referenceDirection.localScale;
    }
}
