using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] private int playerId;
    public int PlayerId => playerId;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool swingHold;

    [SerializeField] private bool holdingWater = false;
    public WaterThrow waterThrow;
    [SerializeField] private Transform boulderMage;
    [SerializeField] private PlayerSounds _sounds;

    public Action<float> ThrowWater;
    
    private void Awake()
    {
        _playerInput = GameObject.Find("Globals").GetComponent<Globals>().PlayerInputs[playerId];
        
        _playerInput.OnADown += SwingPress;
        _playerInput.OnAUp += SwingRelease;
    }

    private void SwingPress()
    {
        swingHold = true;
        _animator.SetBool("SwingHold", swingHold);
        _sounds.Swing();

        if (!holdingWater) return;
        holdingWater = false;
        var direction = transform.localScale.x;
        ThrowWater?.Invoke(direction);
        waterThrow.GetThrown(direction);
        waterThrow = null;
    }

    private void SwingRelease()
    {
        swingHold = false;
        _animator.SetBool("SwingHold", swingHold);
        
        if (holdingWater)
        {
            holdingWater = false;
            var direction = boulderMage.localScale.x;
            ThrowWater?.Invoke(direction);
            //waterThrow.GetThrown(direction);
            //waterThrow = null;
            _animator.SetTrigger("SwingOnce");
            _sounds.Swing();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var OtherTag = other.gameObject.GetComponent<MoonTags>();
        if (OtherTag == null) return;
        CollectWater(other);
    }

    private void CollectWater(Collider2D other)
    {
        if (holdingWater) return;
        if (other.gameObject.GetComponent<MoonTags>().TagList != TagList.Water) return;
        if (other.gameObject.GetComponent<WaterThrow>().IsThrown) return;
        _animator.SetBool("SwingHold", false);
        holdingWater = true;
    }
}
