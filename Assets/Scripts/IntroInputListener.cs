using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroInputListener : MonoBehaviour
{
    private PlayerInput _playerInput;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    [SerializeField] private Animator[] introObjectAnimators = new Animator[4];
    private static readonly int Leave = Animator.StringToHash("Leave");
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject storyIntro;

    void Awake()
    {
        _playerInput = GameObject.Find("Globals").GetComponent<Globals>().PlayerInputs[0];
        StartCoroutine(StartGame());
    }
    
    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);

        while (!_playerInput.AnyButton)
        {
            yield return _waitForFixedUpdate;
        }
        
        PressAnyButton();

        yield return new WaitForSeconds(1.765f);

        Instantiate(storyIntro);
        Destroy(transform.parent.gameObject);
    }

    private void PressAnyButton()
    {
        print(gameObject + " says there has been an input from player 0");
        foreach (var animators in introObjectAnimators)
        {
            animators.SetTrigger(Leave);
        }
        
        music.Stop();
        startSound.Play();
    }

    
}
