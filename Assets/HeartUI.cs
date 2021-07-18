using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private int heartNumber;
    private Animator _animator;
    private PlayerStats _playerStats;
    

    private void Awake()
    {
        var globals = GameObject.Find("Globals").GetComponent<Globals>();
        _playerStats = globals.PlayerStatuses[0];
        _animator = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHeart();
        _playerStats.HeartsChanged += UpdateHeart;
    }

    private void UpdateHeart()
    {
        _animator.SetBool("HeartFull", _playerStats.CurHearts >= heartNumber);
    }
}
