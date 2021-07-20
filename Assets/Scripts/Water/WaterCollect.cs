using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollect : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private CircleCollider2D cc2d;
    private Rigidbody2D rb2d;
    private FollowVector2 _followVector2;
    [SerializeField] private bool held = false;
    public bool Held => held;
    [SerializeField] private WaterThrow _waterThrow;
    private StaffController staff;
    [SerializeField] private WaterSounds _sounds;

    private void Awake()
    {
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        cc2d = gameObject.GetComponent<CircleCollider2D>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        _followVector2 = gameObject.GetComponent<FollowVector2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var OtherTag = other.gameObject.GetComponent<MoonTags>();
        if (OtherTag == null) return;
        if (other.gameObject.GetComponent<MoonTags>().TagList != TagList.Staff) return;
        if (!held)
        {
            held = true;
            gameObject.GetComponent<Animator>().SetBool("isHeld", true);
            bc2d.enabled = false;
            _followVector2.Dom = other.gameObject.transform.Find("ElementHolder");
            _followVector2.StartFollow();
            staff = other.gameObject.GetComponent<StaffController>();
            staff.ThrowWater += _waterThrow.GetThrown;
            cc2d.enabled = true;
            _sounds.Pop();
        }
    }

    private void OnDestroy()
    {
        if (staff != null)
        {
            staff.ThrowWater -= _waterThrow.GetThrown;
        }
    }
}
