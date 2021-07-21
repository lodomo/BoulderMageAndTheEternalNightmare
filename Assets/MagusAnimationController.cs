using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagusAnimationController : MonoBehaviour
{
    [SerializeField] private MagusHealth magusHealth;
    private Animator animator;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetInteger("Health", magusHealth.CurrentHealth);
    }
}
