using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSetter : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Transform respawnLocation;
    [SerializeField] private bool isCurrentSpawner = false;
    private PlayerStats _playerStats;
    

    void Awake()
    {
        _playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[0];
        _playerStats.SetRespawner += UpdateRespawner;
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerStats.SetRespawn(respawnLocation);
        _playerStats.SetRespawner?.Invoke(gameObject);
    }


    private void UpdateRespawner(GameObject gO)
    {
        isCurrentSpawner = gO == gameObject;
        _animator.SetBool("isSpawner", isCurrentSpawner);
    }
}
