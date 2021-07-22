using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossAudio : MonoBehaviour
{
    private AudioSource dungeonMusic;

    private void Awake()
    {
        MusicChange();
    }

    private void MusicChange()
    {
        dungeonMusic = GameObject.Find("Music").GetComponent<AudioSource>();
        
        if (dungeonMusic != null)
        {
            dungeonMusic.Stop();
        }
        
        
    }
}
