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
    private AudioSource _audioSource;
    

    void Awake()
    {
        _playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[0];
        _playerStats.SetRespawner += UpdateRespawner;
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Player) return;
        if (isCurrentSpawner) return;
        _playerStats.SetRespawn(respawnLocation);
        _playerStats.SetRespawner?.Invoke(gameObject);
        _audioSource.Play();
    }


    private void UpdateRespawner(GameObject gO)
    {
        isCurrentSpawner = gO == gameObject;
        _animator.SetBool("isSpawner", isCurrentSpawner);
    }
}
