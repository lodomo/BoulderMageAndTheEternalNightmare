using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHealth : MonoBehaviour
{
    //private int _maxHandHealth = 10;
    private int _currentHandHealth = 10;
    public int CurrentHandHealth => _currentHandHealth;
    [SerializeField] private MagusHealth magusHealth;
    private SpriteRenderer _spriteRenderer;
    private bool canTakeDamage = true;

    [SerializeField] private Animator flashAnimator;
    [SerializeField] private SpriteFlasher[] _spriteFlashers = new SpriteFlasher[6];

    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private AudioSource _handDeath;
    
    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void FakeTriggerEnter(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        HitByDamager(otherTag);
        HitBoulderMage(otherTag);
    }

    private void HitByDamager(MoonTags otherTag)
    {
        if (!canTakeDamage) return;
        
        if (otherTag.TagList == TagList.Staff || otherTag.TagList == TagList.Water)
        {
            StartCoroutine(Co_TakeDamage());
        }
    }

    private IEnumerator Co_TakeDamage()
    {
        canTakeDamage = false;
        if (_currentHandHealth < 1) yield break;
        magusHealth.TakeDamage(1);
        _currentHandHealth -= 1;
        SetFlash();
        
        if (_currentHandHealth > 0)
        {
            _audioSource.Play();
        }
        else
        {
            _handDeath.Play();
        }
        
        _animator.SetInteger("Health", _currentHandHealth);
        yield return new WaitForSeconds(2f);
        canTakeDamage = true;
    }

    private void HitBoulderMage(MoonTags otherTag)
    {
        if (otherTag.TagList == TagList.Player)
        {
            otherTag.gameObject.GetComponent<IDamagable>().TakeDamage(1);
        }
    }

    private void SetFlash()
    {
        foreach (var spriteFlasher in  _spriteFlashers)
        {
            if (_spriteRenderer.sprite == spriteFlasher.ReferenceSprite)
            {
                flashAnimator.SetTrigger(spriteFlasher.name);
            }
        }
    }
}

[System.Serializable]
public class SpriteFlasher
{
    public string name;
    public Sprite ReferenceSprite;
}
