using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private bool isAngry;
    public bool IsAngry => isAngry;
    [SerializeField] private int health;
    [SerializeField] private GameObject deathAnimation;
    [SerializeField] private GameObject treasure;
    private bool hitThisFrame = false;
    private Rigidbody2D _rigidbody2D;
    public int Health => health;

    private Animator _animator;
    private EnemyStage _enemyStage;
    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
        _animator = gameObject.GetComponent<Animator>();
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _enemyStage = transform.parent.GetComponent<EnemyStage>();
        SetAnimHealth();
    }
    public void TakeDamage(int damage)
    {
        if (hitThisFrame) return;
        StartCoroutine(HitBuffer());
        
        health -= damage;
        AngryCheck();
        SetAnimHealth();
        

        if (health >= 0) return;
        
        _enemyStage.EnemyDies?.Invoke();
        Instantiate(deathAnimation, _transform.position, _transform.rotation);
        Instantiate(treasure, _transform.position, _transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator HitBuffer()
    {
        hitThisFrame = true;
        yield return new WaitForSeconds(0.125f);
        hitThisFrame = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TakeStaffDamage(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamageBoulderMage(other);
    }

    private void SetAnimHealth()
    {
        _animator.SetInteger("health", health);
    }

    private void DamageBoulderMage(Collision2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Player) return;
        var damageable = other.gameObject.GetComponent<IDamagable>();
        if (damageable == null) return;
        damageable.TakeDamage(1);
        
    }

    private void TakeStaffDamage(Collider2D other)
    {
        if (other.gameObject.GetComponent<StaffController>() == null) return;
        TakeDamage(1);
        var hitFrom = (Vector2) other.gameObject.transform.position;
        var direction = (Vector2) _transform.position - hitFrom;
        direction = direction.normalized;
        direction.y = .5f;
        _rigidbody2D.velocity = direction * 10;
    }

    private void AngryCheck()
    {
        if (health >= 1) return;
        if (isAngry) return;
        StartCoroutine(Co_AngryCheck());
    }

    private IEnumerator Co_AngryCheck()
    {
        
        yield return new WaitForSeconds(3);
        isAngry = true;
        _animator.SetBool("isAngry", true);
    }
    
    
}
