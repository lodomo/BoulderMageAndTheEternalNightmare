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
    public int CurHearts => curHearts;
    public int Lives => lives;
    public int Coins => coins;

    public void ChangeCurHearts(int i)
    {
        curHearts += i;
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
}
