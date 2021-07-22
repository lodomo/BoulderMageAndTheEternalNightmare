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
    [SerializeField] private GameObject endOfGame;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private GameObject userInterface;

    private void Awake()
    {
        dungeon = GameObject.Find("Dungeon");
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
            yield return _waitForSeconds;
        }

        yield return new WaitForSeconds(8);
        //TODO AT THE RIGHT LOCATION
        Instantiate(endOfGame, gameCamera.transform.position, gameCamera.transform.rotation);
        Destroy(dungeon);
        Destroy(userInterface);
    }
}
