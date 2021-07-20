using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSounds : MonoBehaviour
{
    [SerializeField] private AudioSource pop1;
    [SerializeField] private AudioSource pop2;
    [SerializeField] private AudioSource blob;

    public void Pop()
    {
        var flip = Random.Range(0, 2);
        PlaySound(flip == 0 ? pop1 : pop2);
    }

    public void Blob()
    {
        PlaySound(blob);
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }
}
