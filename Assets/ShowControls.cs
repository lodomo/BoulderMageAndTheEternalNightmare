using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int playerId;
    public int PlayerId => playerId;
    private Animator _animator;

    private void Awake()
    {
        _playerInput = GameObject.Find("Globals").GetComponent<Globals>().PlayerInputs[playerId];
        _animator = gameObject.GetComponent<Animator>();
        _playerInput.OnStartDown += PutControlsOnScreen;
        _playerInput.OnStartUp += RemoveControlsFromScreen;
    }

    private void PutControlsOnScreen()
    {
        _animator.SetBool("OnScreen", true);
    }
    
    private void RemoveControlsFromScreen()
    {
        _animator.SetBool("OnScreen", false);
    }
}
