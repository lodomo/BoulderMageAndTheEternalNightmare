using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour, IDamagable, IHealable
{
    private int playerID = 0;
    [SerializeField] private PlayerStats _playerStats;

    void Awake()
    {
        var globals = GameObject.Find("Globals").GetComponent<Globals>();
        _playerStats = globals.PlayerStatuses[playerID];
    }

    public void TakeDamage(int damage)
    {
        print("Boulder Mage Took Damage");
        _playerStats.ChangeCurHearts(-damage);
    }

    public void GiveHealth(int health)
    {
        print("Boulder Mage Healed");
        _playerStats.ChangeCurHearts(health);
    }
}
