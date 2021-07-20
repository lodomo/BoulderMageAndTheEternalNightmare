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
    private BoulderController _boulderController;
    private WaitForSeconds WaitForSpawnTime = new WaitForSeconds(1);
    private Animator animator;
    [SerializeField] private GameObject deathAnimation;

    void Awake()
    {
        var globals = GameObject.Find("Globals").GetComponent<Globals>();
        _playerStats = globals.PlayerStatuses[playerID];
        _transform = transform;
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _boulderController = gameObject.GetComponent<BoulderController>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        print("Boulder Mage Took Damage");
        _playerStats.ChangeCurHearts(-damage);
        Instantiate(deathAnimation, _transform.position, _transform.rotation);
        _transform.position = _playerStats.ReSpawnLocation.position;
        _rigidbody2D.velocity = Vector2.zero;
        
        //TODO Respawn animation, death animation, movement stopper
        StartCoroutine(SpawnController());

        if (_playerStats.CurHearts == 0)
        {
            StartCoroutine(DeathReset());
            _playerStats.ChangeDeaths(1);
        }
    }

    private IEnumerator DeathReset()
    {
        yield return new WaitForSeconds(1);
        _playerStats.ChangeCurHearts(3);
    }

    private IEnumerator SpawnController()
    {
        _boulderController.isSpawning = true;
        animator.SetTrigger("Spawn");
        yield return WaitForSpawnTime;
        _boulderController.isSpawning = false;
    }

    public void GiveHealth(int health)
    {
        print("Boulder Mage Healed");
        _playerStats.ChangeCurHearts(health);
    }
}
