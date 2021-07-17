using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovement : MonoBehaviour
{
    [SerializeField] private Vector2 RoomRadius = new Vector2(19, 13);
    [SerializeField] private RoomInformation currentRoom;
    public RoomInformation CurrentRoom => currentRoom;
    [SerializeField] public RoomInformation[] rooms = new RoomInformation[11];
    private Transform boulderMage;
    private Rigidbody2D boulderMageRB2D;
    private Transform mainCamera;
    private float cameraLerpTime = 0.5f;

    public Action RoomChange;

    // Start is called before the first frame update
    void Start()
    {
        var bm = GameObject.Find("BoulderMage");
        boulderMage = bm.transform;
        boulderMageRB2D = bm.GetComponent<Rigidbody2D>();
        currentRoom = FindCurrentRoom();
        mainCamera = GameObject.Find("GameCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InTheRoomCheck();
    }

    private RoomInformation FindCurrentRoom()
    {
        var roomFinder = new RoomInformation();
        foreach (var room in rooms)
        {
            var bmLoc = (Vector2) boulderMage.position;
            var roomLoc = (Vector2) room.RoomLocation.position;
            var checkBoundaries = new RoomBoundaries
            {
                Left = roomLoc.x - RoomRadius.x,
                Right = roomLoc.x + RoomRadius.x,
                Up = roomLoc.y + RoomRadius.y,
                Down = roomLoc.y - RoomRadius.y
            };

            if (bmLoc.x > checkBoundaries.Right) continue;
            if (bmLoc.x < checkBoundaries.Left) continue;
            if (bmLoc.y < checkBoundaries.Down) continue;
            if (bmLoc.y > checkBoundaries.Up) continue;
            roomFinder = room;
        }
        
        return roomFinder;
    }

    private void InTheRoomCheck()
    {
        var bmLoc = (Vector2) boulderMage.position;
        var roomLoc = (Vector2) currentRoom.RoomLocation.position;
        var checkBoundaries = new RoomBoundaries
        {
            Left = roomLoc.x - RoomRadius.x,
            Right = roomLoc.x + RoomRadius.x,
            Up = roomLoc.y + RoomRadius.y,
            Down = roomLoc.y - RoomRadius.y
        };

        if (bmLoc.x > checkBoundaries.Right || bmLoc.x < checkBoundaries.Left || bmLoc.y < checkBoundaries.Down || bmLoc.y > checkBoundaries.Up)
        {
            currentRoom = FindCurrentRoom();
            if (bmLoc.y > checkBoundaries.Up)
            {
                StartCoroutine(MoveCamera(true));
            }
            else
            {
                StartCoroutine(MoveCamera(false));
            }
        }
    }

    private IEnumerator MoveCamera(bool launch)
    {
        var currentLerpTime = 0f;
        var startPos = mainCamera.position;
        var endPos = currentRoom.RoomLocation.position;
        endPos.z = startPos.z;
        
        Time.timeScale = 0;

        while (currentLerpTime < cameraLerpTime)
        {
            //increment timer once per frame
            currentLerpTime += Time.unscaledDeltaTime;
            if (currentLerpTime > cameraLerpTime) {
                currentLerpTime = cameraLerpTime;
            }
 
            //lerp!
            float perc = currentLerpTime / cameraLerpTime;
            mainCamera.position = Vector3.Lerp(startPos, endPos, perc);
            yield return new WaitForEndOfFrame();
        }
        
        Time.timeScale = 1;
        
        RoomChange?.Invoke();

        if (!launch) yield break;
        print("BOULDER UP!");
        boulderMageRB2D.velocity = new Vector2(0, 30f);
    }
}

[System.Serializable]
public class RoomInformation
{
    public string Name;
    public Transform RoomLocation;
    public bool isCleared;
}

public class RoomBoundaries
{
    public float Left;
    public float Right;
    public float Up;
    public float Down;
}