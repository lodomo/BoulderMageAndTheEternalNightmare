using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public Action OnWall;
    public Action OffWall;

    [SerializeField] private bool _onWallBool;
    public bool onWallBool => _onWallBool;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Stage) return;
        OnWall?.Invoke();
        _onWallBool = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Stage) return;
        OffWall?.Invoke();
        _onWallBool = false;
    }
}
