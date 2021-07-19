using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class Flood : MonoBehaviour
{
    [SerializeField] private FloodCheckers belowChecker;
    [SerializeField] private FloodCheckers rightChecker;
    [SerializeField] private FloodCheckers leftChecker;
    [SerializeField] private GameObject recursiveFlood;
    
    private Animator _animator;

    private bool oldFlooder = false;
    private const float floodForce = 2f;
    private Transform _transform;
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _transform = transform;
        _boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }
    
    

    private void Start()
    {

        StartCoroutine(FurtherTheFlood());
    }

    private IEnumerator FurtherTheFlood()
    {
        yield return new WaitForSeconds(0.025f);
        if (!belowChecker.CheckerFloodChecker.StageCheck)
        {
            _animator.SetTrigger("DownFlood");
            var newFlood = Instantiate(recursiveFlood, belowChecker.CheckerTransform.position, belowChecker.CheckerTransform.rotation);
            print("Make a flood below");
        }
        else if (!belowChecker.CheckerFloodChecker.FloodCheck)
        {
            if (!rightChecker.CheckerFloodChecker.StageCheck && !rightChecker.CheckerFloodChecker.FloodCheck)
            {
                var newFlood = Instantiate(recursiveFlood, rightChecker.CheckerTransform.position,
                    rightChecker.CheckerTransform.rotation);
                
                print("Make a flood to right");
            }

            if (!leftChecker.CheckerFloodChecker.StageCheck && !leftChecker.CheckerFloodChecker.FloodCheck)
            {
                var newFlood = Instantiate(recursiveFlood, leftChecker.CheckerTransform.position,
                    leftChecker.CheckerTransform.rotation);
                print("Make a flood to left");
            }
        }

        oldFlooder = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (oldFlooder) return;
        var getTag = other.gameObject.GetComponent<MoonTags>();
        if (getTag != null && getTag.TagList == TagList.Player) return;
        var damagable = other.gameObject.GetComponent<IDamagable>();
        
        damagable?.TakeDamage(10);

        //var enemyRb2d = getEnemy.GetComponent<Rigidbody2D>();

        // if (_downFlooder)
        // {
        //     print("DownFlooder");
        //     enemyRb2d.velocity = Vector2.down * floodForce;
        // }
        // else
        // {
        //     var enemyPosit = getEnemy.transform.position;
        //     var direction = (Vector2) (enemyPosit - _transform.position);
        //     enemyRb2d.velocity = direction * floodForce;
        // }
    }
}

[System.Serializable]
public class FloodCheckers
{
    public GameObject CheckerObject;
    public FloodChecker CheckerFloodChecker;
    public Transform CheckerTransform;
}
