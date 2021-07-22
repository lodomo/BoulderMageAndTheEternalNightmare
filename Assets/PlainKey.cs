using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainKey : MonoBehaviour
{
    private AudioSource _audioSource;
    private FollowVector2 _followVector2;

    private void Awake()
    {
        _followVector2 = gameObject.GetComponent<FollowVector2>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_followVector2.isFollowing) return;
        
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Player) return;
        _audioSource.Play();
        var dom = other.gameObject.transform.Find("KeyLocation").transform;
        
        _followVector2.Dom = dom;
        _followVector2.isFollowing = true;
        _followVector2.StartFollow();
    }
}
