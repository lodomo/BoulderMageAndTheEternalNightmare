using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject BossKey;
    [SerializeField] private Transform keySpawn;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList == TagList.PlainKey)
        {
            _animator.SetTrigger("OpenChest");
            Destroy(otherTag.gameObject);
            _audioSource.Play();
            Instantiate(BossKey, keySpawn.position, keySpawn.rotation);
        }
    }
}
