using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGameStats : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Sprite[] numbers = new Sprite[10];

    [SerializeField] private SpriteRenderer coinCount10;
    [SerializeField] private SpriteRenderer coinCount1;
    
    [SerializeField] private SpriteRenderer deathcount10;
    [SerializeField] private SpriteRenderer deathcount1;
    
    [SerializeField] private SpriteRenderer timecountH;
    [SerializeField] private SpriteRenderer timecountM10;
    [SerializeField] private SpriteRenderer timecountM1;
    [SerializeField] private SpriteRenderer timecountS10;
    [SerializeField] private SpriteRenderer timecountS1;
    

    void Awake()
    {
        _playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[0];
        _playerStats.FinishGameTimer();

        coinCount10.sprite = numbers[_playerStats.Coins / 10];
        coinCount1.sprite = numbers[_playerStats.Coins % 10];
        
        deathcount10.sprite = numbers[_playerStats.Deaths / 10];
        deathcount1.sprite = numbers[_playerStats.Deaths % 10];
            
        timecountH.sprite = numbers[_playerStats.FinalPlayTime.hour];
        timecountM10.sprite = numbers[_playerStats.FinalPlayTime.minute / 10];
        timecountM1.sprite = numbers[_playerStats.FinalPlayTime.minute % 10];
        timecountS10.sprite = numbers[_playerStats.FinalPlayTime.second / 10];
        timecountS1.sprite = numbers[_playerStats.FinalPlayTime.second % 10];
    }
}
