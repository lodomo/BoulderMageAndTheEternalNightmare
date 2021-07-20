using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gooper : MonoBehaviour
{
    private int rng = 0;
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            rng = (int) Random.Range(1, 5);
            if (rng == 3)
            {
                _animator.SetTrigger("Reset");
            }

            var random2 = Random.Range(0f, 1f);
            yield return new WaitForSeconds(random2);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag != null && otherTag.TagList == TagList.Player)
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(1);
        }
    }
}
