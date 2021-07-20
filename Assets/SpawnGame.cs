using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGame : MonoBehaviour
{
    [SerializeField] private GameObject dungeon;
    void Awake()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        var _transform = transform;
        yield return new WaitForSeconds(12.5f);
        Instantiate(dungeon, _transform.position, _transform.rotation);
        Destroy(gameObject);
    }
}
