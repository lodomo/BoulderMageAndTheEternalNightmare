using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandController : MonoBehaviour
{
    
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private Animator leftAnimator;
    private Animator rightAnimator;

    private HandHealth leftHandHealth;
    private HandHealth rightHandHealth;

    private WaitForSeconds waitForNextMove = new WaitForSeconds(4);

    private void Awake()
    {
        leftAnimator = leftHand.GetComponent<Animator>();
        rightAnimator = rightHand.GetComponent<Animator>();
        
        leftHandHealth = leftHand.GetComponent<HandHealth>();
        rightHandHealth = rightHand.GetComponent<HandHealth>();
    }

    private void Start()
    {
        StartCoroutine(BossFight());
    }

    private IEnumerator BossFight()
    {
        yield return new WaitForSeconds(10);
        while (true)
        {
            var chooseHand = Random.Range(0, 2);
            if (chooseHand == 1)
            {
                if (leftHandHealth.CurrentHandHealth > 1)
                {
                    LeftHandMoves();
                }
                else
                {
                    RightHandMoves();
                }
            }
            else
            {
                if (rightHandHealth.CurrentHandHealth > 1)
                {
                    RightHandMoves();
                }
                else
                {
                    LeftHandMoves();
                }
            }
        
            yield return waitForNextMove;
        }
    }

    private void LeftHandMoves()
    {
        var chooseMove = Random.Range(0, 2);
        leftAnimator.SetTrigger(chooseMove == 1 ? "SlamGround" : "Slap");
    }

    private void RightHandMoves()
    {
        var chooseMove = Random.Range(0, 2);
        rightAnimator.SetTrigger(chooseMove == 1 ? "Point" : "Punch");
    }
}
