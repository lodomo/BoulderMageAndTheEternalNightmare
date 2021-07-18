using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHearts = 3;
    [SerializeField] private int curHearts = 3;
    [SerializeField] private int lives = 3;
    [SerializeField] private int coins = 0;
    [SerializeField] private Transform reSpawnLocation;
    public int CurHearts => curHearts;
    public int Lives => lives;
    public int Coins => coins;

    public Action HeartsChanged;
    public Action<GameObject> SetRespawner;

    public void ChangeCurHearts(int i)
    {
        curHearts += i;

        if (curHearts > maxHearts)
        {
            curHearts = maxHearts;
        }

        if (curHearts < 0)
        {
            curHearts = 0;
        }
        
        HeartsChanged?.Invoke();
    }

    public void ChangeLives(int i)
    {
        lives += i;
    }
    
    public void ChangeCoins(int i)
    {
        coins += i;
        
        //TODO Lives from coins.
    }

    public void SetRespawn(Transform newRespawn)
    {
        reSpawnLocation = newRespawn;
    }
}
