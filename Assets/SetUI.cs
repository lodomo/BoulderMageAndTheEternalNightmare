using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUI : MonoBehaviour
{
    void Awake()
    {
        var gameCamera = GameObject.Find("GameCamera");
        transform.parent = gameCamera.transform;
    }
}
