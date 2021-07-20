using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounting : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] private Sprite[] numbers = new Sprite[10];
    [SerializeField] private SpriteRenderer onesPlace;
    [SerializeField] private SpriteRenderer tensPlace;
    private PlayerStats playerStats;
    private int _displayedDeaths;
    [SerializeField] private AudioSource deathCounter;
    private Animator _animator;

    void Awake()
    {
        playerStats = GameObject.Find("Globals").GetComponent<Globals>().PlayerStatuses[playerID];
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        playerStats.DeathsChanged += UpdateDeaths;
    }

    private IEnumerator Co_UpdateDeaths()
    {
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        deathCounter.Play();
        onesPlace.sprite = numbers[playerStats.Deaths % 10];
        tensPlace.sprite = numbers[playerStats.Deaths / 10];
    }

    private void UpdateDeaths()
    {
        StartCoroutine(Co_UpdateDeaths());
    }
}
