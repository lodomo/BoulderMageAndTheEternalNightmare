using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGrow : MonoBehaviour
{
    [SerializeField] private WaterCollect _waterCollect;
    [SerializeField] private int waterSize = 0;
    [SerializeField] private GameObject waterPop;
    public int WaterSize => waterSize;
    private Animator _animator;
    private static readonly int Size = Animator.StringToHash("size");
    private Transform _transform;
    [SerializeField] private WaterSounds _sounds;
    

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _transform = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList != TagList.Water) return;
        
        if (_waterCollect.Held)
        {
            waterSize += (other.gameObject.GetComponent<WaterGrow>().WaterSize);
            print("Add Water");
            AdjustWaterSize();
                        
            if (waterSize == 5)
            {
                _sounds.Blob();
            }
        }
        else
        {
            Instantiate(waterPop, _transform.position, _transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherTag = other.gameObject.GetComponent<MoonTags>();
        if (otherTag == null) return;
        if (otherTag.TagList == TagList.Stage) return;
        if (otherTag.TagList == TagList.Player) return;
        Instantiate(waterPop, _transform.position, _transform.rotation);
        other.gameObject.GetComponent<IDamagable>()?.TakeDamage(1);
        Destroy(gameObject);
    }

    private void AdjustWaterSize()
    {
        _animator.SetInteger(Size, (int)waterSize);
    }

    private void GrowWater(Collision2D other)
    {
        
    }
}
