using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderGetCoins : MonoBehaviour, ICanGetCoins
{
    private PlayerStats _playerStats;

    private void Awake()
    {
        _playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[0];
    }
    public void GetCoin()
    {
        //TODO ACTUALL ADD IN COINS
        _playerStats.ChangeCoins(1);
        print("Boulder Got A Coin");
    }
}
