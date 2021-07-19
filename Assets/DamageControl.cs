using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour, IDamagable, IHealable
{
    private int playerID = 0;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject parent;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;

    void Awake()
    {
        var globals = GameObject.Find("Globals").GetComponent<Globals>();
        _playerStats = globals.PlayerStatuses[playerID];
        _transform = transform;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        print("Boulder Mage Took Damage");
        _playerStats.ChangeCurHearts(-damage);

        _transform.position = _playerStats.ReSpawnLocation.position;
        _rigidbody2D.velocity = Vector2.zero;
        
        //TODO Respawn animation, death animation, movement stopper
    }

    public void GiveHealth(int health)
    {
        print("Boulder Mage Healed");
        _playerStats.ChangeCurHearts(health);
    }
}
