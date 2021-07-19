using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounting : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] private Sprite[] numbers = new Sprite[10];
    [SerializeField] private SpriteRenderer onesPlace;
    [SerializeField] private SpriteRenderer tensPlace;
    private PlayerStats playerStats;
    private int _displayedCoins;

    void Awake()
    {
        playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[playerID];
        
    }

    private void Start()
    {
        UpdateCoins();
        playerStats.CoinsChanged += UpdateCoins;
    }

    private void UpdateCoins()
    {
        onesPlace.sprite = numbers[playerStats.Coins % 10];
        tensPlace.sprite = numbers[playerStats.Coins / 10];
    }
}
