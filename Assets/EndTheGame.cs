using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EndTheGame : MonoBehaviour
{
    [SerializeField] private GameObject dungeon;
    [SerializeField] private MagusHealth magusHealth;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(1);
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField] private GameObject endOfGame;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private GameObject userInterface;
    [SerializeField] private AudioSource magusDeathSound;

    private void Awake()
    {
        dungeon = GameObject.FindGameObjectWithTag("Dungeon");
        gameCamera = GameObject.Find("GameCamera");
        userInterface = GameObject.Find("UserInterface");
        
    }

    private void Start()
    {
        StartCoroutine(CheckHealth());
    }

    private IEnumerator CheckHealth()
    {
        while (magusHealth.CurrentHealth > 0)
        {
            yield return _waitForFixedUpdate;
        }

        yield return new WaitForSeconds(2);
        magusDeathSound.Play();
        yield return new WaitForSeconds(6);
        var spawnLocation = gameCamera.transform.position;
        spawnLocation.z = 0;
        Instantiate(endOfGame, spawnLocation, gameCamera.transform.rotation);
        Destroy(dungeon);
        Destroy(userInterface);
    }
}
