using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodChecker : MonoBehaviour
{
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private bool stageCheck = false;
    public bool StageCheck => stageCheck;
    
    [SerializeField] private bool floodCheck = false;
    public bool FloodCheck => floodCheck;

    private void Awake()
    {
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList == TagList.Stage)
        {
            stageCheck = true;
        }
        else if (otherTag.TagList == TagList.Flood)
        {
            floodCheck = true;
        }
        
    }
}
