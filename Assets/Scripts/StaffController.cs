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
    }

    private void SwingRelease()
    {
        swingHold = false;
        _animator.SetBool("SwingHold", swingHold);
    }


}
