using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource wallJump;
    [SerializeField] private AudioSource doubleJump;
    [SerializeField] private AudioSource hitGround;
    [SerializeField] private AudioSource swing;

    public void Jump()
    {
        PlaySound(jump);
    }
    
    public void WallJump()
    {
        PlaySound(wallJump);
    }
    
    public void DoubleJump()
    {
        PlaySound(doubleJump);
    }
    
    public void HitGround()
    {
        PlaySound(hitGround);
    }
    
    public void Swing()
    {
        PlaySound(swing);
    }
    

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
}
