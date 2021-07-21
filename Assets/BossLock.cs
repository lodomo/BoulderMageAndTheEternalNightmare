using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLock : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        Unlock(otherTag);
    }

    private void Unlock(MoonTags otherTag)
    {
        if (otherTag.TagList != TagList.BossKey) return;
        Destroy(otherTag.gameObject);
        Destroy(gameObject);
    }
}
