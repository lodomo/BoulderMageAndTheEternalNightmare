using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStage : MonoBehaviour
{
    [SerializeField] private int enemyCount;
    private WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();

    public Action EnemyDies;

    private RoomData _roomData;

    private void Awake()
    {
        _roomData = transform.parent.GetComponent<RoomData>();
    }


    void Start()
    {
        EnemyDies += CheckClearStage;
        CountAllEnemiesInChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountAllEnemiesInChildren()
    {
        var children = GetComponentsInChildren(typeof(Enemy));
        enemyCount = children.Length;
        print("There are " + enemyCount + " enemies in this room");
    }

    private void ClearScene()
    {
        if (enemyCount > 0) return;
        _roomData.RoomClear();

    }

    private IEnumerator Co_CheckClearStage()
    {
        yield return _endOfFrame;
        CountAllEnemiesInChildren();
        ClearScene();
    }

    private void CheckClearStage()
    {
        StartCoroutine(Co_CheckClearStage());
    }
}
