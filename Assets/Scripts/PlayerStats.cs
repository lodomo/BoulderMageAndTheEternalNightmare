using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHearts = 3;
    [SerializeField] private int curHearts = 3;
    [SerializeField] private int deaths = 0;
    [SerializeField] private int coins = 0;
    [SerializeField] private Transform reSpawnLocation;
    public Transform ReSpawnLocation => reSpawnLocation;
    public int CurHearts => curHearts;
    public int Deaths => deaths;
    public int Coins => coins;

    public Action HeartsChanged;
    public Action<GameObject> SetRespawner;
    public Action DeathsChanged;

    public Action CoinsChanged;

    [SerializeField] private bool hasNormalKey;
    [SerializeField] private bool hasBossKey;
    public bool HasNormalKey => hasNormalKey;
    public bool HasBossKey => hasBossKey;

    [SerializeField] private MoonFinishTime finalPlayTime;
    public MoonFinishTime FinalPlayTime => finalPlayTime;
    [SerializeField] private GamePlayTimer _gamePlayTimer;

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

    public void ChangeDeaths(int i)
    {
        deaths += i;
        DeathsChanged?.Invoke();
    }
    
    public void ChangeCoins(int i)
    {
        coins += i;
        CoinsChanged?.Invoke();
        //TODO Lives from coins.
    }

    public void SetRespawn(Transform newRespawn)
    {
        reSpawnLocation = newRespawn;
    }

    public void FinishGameTimer()
    {
        finalPlayTime = _gamePlayTimer.GameFinish();
    }
}
