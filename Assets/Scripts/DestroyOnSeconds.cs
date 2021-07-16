using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSeconds : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0f;
    private WaitForSeconds _destroyTimer;
    
    // Start is called before the first frame update
    void Awake()
    {
        _destroyTimer = new WaitForSeconds(destroyTime);
    }

    void Start()
    {
        StartCoroutine(DestroyMeSenpai());
    }

    private IEnumerator DestroyMeSenpai()
    {
        yield return _destroyTimer;
        Destroy(gameObject);
    }
}
