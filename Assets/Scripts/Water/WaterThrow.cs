using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterThrow : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private const float ThrowStrength = 20f;
    private FollowVector2 _followVector2;
    private bool isThrown = false;
    public bool IsThrown => isThrown;
    [SerializeField] private GameObject waterPop;
    private Transform _transform;
    [SerializeField] private WaterGrow _waterGrow;
    [SerializeField] private GameObject flood;
    [SerializeField] private GameObject floodSound;

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _followVector2 = gameObject.GetComponent<FollowVector2>();
        _transform = transform;
    }
    public void GetThrown(float dir)
    {
        var throwPosition = transform.position;
        throwPosition.y -= 1.25f;
        transform.position = throwPosition;
        
        _followVector2.isFollowing = false;
        if (_waterGrow.WaterSize >= 5)
        {
            _rigidbody2D.gravityScale = 1;
            _rigidbody2D.AddForce(new Vector2(dir * ThrowStrength/ 1.5f, 0), ForceMode2D.Impulse);
        }
        else
        {
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = new Vector2(dir * ThrowStrength, 0); 
        }
        
        isThrown = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (!isThrown) return;
        var othertag = other.gameObject.GetComponent<MoonTags>();
        if (othertag == null) return;
        if (othertag.TagList == TagList.Staff) return;
        if (othertag.TagList == TagList.Player) return;

        if (othertag.TagList == TagList.Enemy || isThrown)
        {
            Instantiate(waterPop, _transform.position, _transform.rotation);
            var damagable = other.GetComponent<IDamagable>();
            damagable?.TakeDamage(1);
            CheckFlood();
            Destroy(gameObject);
        }
        
    }

    private void CheckFlood()
    {
        if (_waterGrow.WaterSize < 5) return;
        var spawnPoint = _transform.position;
        var xLocation = spawnPoint.x;
        var yLocation = spawnPoint.y;

        var xScraps = xLocation % 1;
        var yScraps = yLocation % 1;

        xLocation -= xScraps;
        yLocation -= yScraps;

        if (_rigidbody2D.velocity.x > 0)
        {
            xLocation += 0.5f;
        }
        else
        {
            xLocation -= 0.5f;
        }

        yLocation += 0.5f;

        Instantiate(flood, new Vector3(xLocation, yLocation, 0), _transform.rotation);
        Instantiate(floodSound);

    }
}
