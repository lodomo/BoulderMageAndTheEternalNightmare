using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.Demos;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private Player _player; // The Rewired Player
    public int playerId = 0;

    [SerializeField] private Vector2 dPad = new Vector2();
    public Vector2 DPad => dPad;
    [SerializeField] private bool aButton = false;
    public bool AButton => aButton;
    [SerializeField] private bool bButton = false;
    public bool BButton => bButton;
    [SerializeField] private bool startButton = false;
    public bool StartButton => startButton;
    [SerializeField] private bool anyButton = false;
    public bool AnyButton => anyButton;

    private buttonStates _aState = new buttonStates();
    private buttonStates _bState = new buttonStates();
    private buttonStates _startState = new buttonStates();
    
    public Action OnADown;
    public Action OnAUp;
    public Action OnBDown;
    public Action OnBUp;
    public Action OnStartDown;
    public Action OnStartUp;
    
    void Awake() {
        _player = ReInput.players.GetPlayer(playerId);
    }

    void Update () {
        GetInput();
        ProcessInput();
    }

    private void ProcessInput()
    {
        InvokeAction(_aState.down, OnADown);
        InvokeAction(_aState.up, OnAUp);
        InvokeAction(_bState.down, OnBDown);
        InvokeAction(_bState.up, OnBUp);
        InvokeAction(_startState.down, OnStartDown);
        InvokeAction(_startState.up, OnStartUp);
    }

    private void GetInput() {
        
       
        
        //THIS IS FOR DEBUGGING.
        aButton = _player.GetButton("A");
        bButton = _player.GetButton("B");
        startButton = _player.GetButton("Start");
        anyButton = CheckForAnyButton();
        
        //THIS IS FOR ACTUAL DOING STUFF
        dPad.x = _player.GetAxis("Horizontal");
        dPad.y = _player.GetAxis("Vertical");
        
        _aState.down = _player.GetButtonDown("A");
        _aState.up = _player.GetButtonUp("A");
        
        _bState.down = _player.GetButtonDown("B");
        _bState.up = _player.GetButtonUp("B");
        
        _startState.down = _player.GetButtonDown("Start");
        _startState.up = _player.GetButtonUp("Start");
    }

    private bool CheckForAnyButton()
    {
        return aButton || bButton || startButton;
    }
    
    private void InvokeAction(bool b, Action a)
    {
        if (!b) return;
        a?.Invoke();
        //print("Button Pressed");
    }
}

public class buttonStates
{
    public bool down;
    public bool up;
}