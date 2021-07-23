using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePlayTimer : MonoBehaviour
{
    private DateTime gameStart;
    private DateTime gameEnd;
    private TimeSpan finishTime;
    [SerializeField] private PlayerStats _playerStats;
    private void Start()
    {
        gameStart = DateTime.Now;
    }

    public MoonFinishTime GameFinish()
    {
        
        gameEnd = DateTime.Now;

        finishTime = gameEnd - gameStart;

        var moonTime = new MoonFinishTime
        {
            hour = finishTime.Hours,
            minute = finishTime.Minutes,
            second = finishTime.Seconds,
            millisecond = finishTime.Milliseconds
        };

        return moonTime;


    }
}

[System.Serializable]
public class MoonFinishTime
{
    public int hour;
    public int minute;
    public int second;
    public int millisecond;
}
