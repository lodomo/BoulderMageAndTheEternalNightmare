using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPop : MonoBehaviour
{
    [SerializeField] private WaterSounds _sounds;

    void Awake()
    {
        _sounds.Pop();
    }

}
