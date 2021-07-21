using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleBrick : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       var otherTag = other.gameObject.GetComponent<MoonTags>();
       if (otherTag == null) return;
       if (otherTag.TagList == TagList.Player)
       {
           _animator.SetTrigger("Crumble");
           _audioSource.Play();
       }
    }
}
