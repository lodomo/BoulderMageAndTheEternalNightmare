using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class Flood : MonoBehaviour
{
    [SerializeField] private FloodCheckers belowChecker;
    [SerializeField] private FloodCheckers rightChecker;
    [SerializeField] private FloodCheckers leftChecker;
    [SerializeField] private GameObject recursiveFlood;

    private void Start()
    {
        StartCoroutine(FurtherTheFlood());
    }

    private IEnumerator FurtherTheFlood()
    {
        yield return new WaitForSeconds(0.1f);
        if (!belowChecker.CheckerFloodChecker.StageCheck)
        {
            Instantiate(recursiveFlood, belowChecker.CheckerTransform.position, belowChecker.CheckerTransform.rotation);
            print("Make a flood below");
        }
        else if (!belowChecker.CheckerFloodChecker.FloodCheck)
        {
            
            if (!rightChecker.CheckerFloodChecker.StageCheck && !rightChecker.CheckerFloodChecker.FloodCheck)
            {
                Instantiate(recursiveFlood, rightChecker.CheckerTransform.position,
                    rightChecker.CheckerTransform.rotation);
                print("Make a flood to right");
            }

            if (!leftChecker.CheckerFloodChecker.StageCheck && !leftChecker.CheckerFloodChecker.FloodCheck)
            {
                Instantiate(recursiveFlood, leftChecker.CheckerTransform.position,
                    leftChecker.CheckerTransform.rotation);
                print("Make a flood to left");
            }
        }
    }
        
}

[System.Serializable]
public class FloodCheckers
{
    public GameObject CheckerObject;
    public FloodChecker CheckerFloodChecker;
    public Transform CheckerTransform;
}
