using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class MagusPupils : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform boulderMage;
    [SerializeField] private Vector2 moveVector;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        boulderMage = GameObject.Find("BoulderMage").transform;
        _transform = transform;
    }

    private Vector2 GetPupilVector()
    {
        var currentPosition = (Vector2) centerPoint.position;
        var bmPosition = (Vector2) boulderMage.position;
        var rawVector = (bmPosition - currentPosition);
        var normalizedVector = (bmPosition - currentPosition).normalized;

        if (Math.Sqrt((rawVector.x * rawVector.x) + (rawVector.y * rawVector.y)) < 0.5f)
        {
            return rawVector;
        }

        return normalizedVector;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.localPosition = GetPupilVector();
    }
}
