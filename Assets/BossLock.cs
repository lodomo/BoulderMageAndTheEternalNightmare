using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLock : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        Unlock(otherTag);
    }

    private void Unlock(MoonTags otherTag)
    {
        if (otherTag.TagList != TagList.BossKey) return;
        Destroy(otherTag.gameObject);
        _boxCollider2D.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.Play();
    }
}
