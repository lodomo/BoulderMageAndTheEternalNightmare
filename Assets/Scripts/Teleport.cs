using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform sisterPortal;
    private Teleport sisterPortalTeleport;
    [SerializeField] private bool verticalWarp;
    [SerializeField] private bool horizontalWarp;
    //[HideInInspector] public GameObject LastTeleportee;
    
    private void Awake()
    {
        sisterPortalTeleport = sisterPortal.gameObject.GetComponent<Teleport>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //if (other.gameObject == LastTeleportee) return;
        if (other.gameObject.GetComponent<Warpable>() == null) return;
        
        var teleportee = other.gameObject.transform.position;
        if (verticalWarp) { teleportee.y = sisterPortal.position.y; }
        if (horizontalWarp) { teleportee.x = sisterPortal.position.x; }
        //sisterPortalTeleport.LastTeleportee = other.gameObject;
        other.gameObject.transform.position = teleportee;
        //print(gameObject.name + "says Teleport Successful!");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //LastTeleportee = null;
    }
}
