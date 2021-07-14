using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnNextAndDelete : MonoBehaviour
{
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private GameObject thingToSpawn;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitTimer());
    }

    private IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(thingToSpawn);
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }
}
