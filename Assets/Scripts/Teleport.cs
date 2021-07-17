using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform sisterPortal;
    private Teleport sisterPortalTeleport;

    private void Awake()
    {
        sisterPortalTeleport = sisterPortal.gameObject.GetComponent<Teleport>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Warpable>() == null) return;
        var sisterVector2 = sisterPortal.position;
        var otherObject = other.gameObject;
        var teleportee = otherObject.transform.position;
        
        teleportee.y = sisterVector2.y;
        teleportee.x = sisterVector2.x;
        otherObject.transform.position = teleportee;
        StartCoroutine(TeleportDelay());
    }

    private IEnumerator TeleportDelay()
    {
        var sPbc2d = sisterPortal.gameObject.GetComponent<BoxCollider2D>();
        sPbc2d.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sPbc2d.enabled = true;
    }
}
