using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagusHealth : MonoBehaviour
{
    private const int MaxHealth = 20;
    private int currentHealth = 20;
    public int CurrentHealth => currentHealth;
    [SerializeField] private Animator[] healthBars = new Animator[10];

    void CheckHealth()
    {
        for (var i = 0; i < 10; i++)
        {
            var currentRange = (i * 2) + 2;
            print("Checked Magus Health");
            if (currentHealth < currentRange)
            {
                healthBars[i].SetTrigger("Damage");
            }

            if (currentHealth < (currentRange - 1))
            {
                healthBars[i].SetTrigger("Empty");
            }
        }
}
    
    // Start is called before the first frame update
    void Start()
    {
        CheckHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }
}
