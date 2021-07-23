using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _words;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Player) return;
        _words.enabled = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Player) return;
        _words.enabled = false;
    }
}
