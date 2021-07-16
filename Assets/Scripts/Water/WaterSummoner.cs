using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSummoner : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private Transform summonLocation;
    [SerializeField] private float summonDelay;
    private WaitForSeconds wfsSummonDelay;

    private void Awake()
    {
        wfsSummonDelay = new WaitForSeconds(summonDelay);
    }

    private void Start()
    {
        StartCoroutine(SummonWater());
        
    }

    private IEnumerator SummonWater()
    {
        while (true)
        {
            Instantiate(water, summonLocation.position, summonLocation.rotation);
            yield return wfsSummonDelay;
        }
    }
}
