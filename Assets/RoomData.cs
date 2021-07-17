using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    [SerializeField] private int roomNumberInArray;
    [SerializeField] private RoomMovement roomControllerRoomMovement;
    [SerializeField] private RoomInformation thisRoom;
    [SerializeField] private GameObject clearedRoom;
    [SerializeField] private GameObject startRoom;

    private GameObject spawnedRoom;

    void Start()
    {
        thisRoom = roomControllerRoomMovement.rooms[roomNumberInArray];
        roomControllerRoomMovement.RoomChange += RoomCheck;
    }

    private void RoomCheck()
    {
        if (thisRoom.Name != roomControllerRoomMovement.CurrentRoom.Name) return;
        print(gameObject.name + " says the current room is " + thisRoom.Name);
        CreateRoomAssets();
    }

    private void CreateRoomAssets()
    {
        if (startRoom == null) return;
        StartCoroutine(Co_CreateRoomAssets());
    }

    private IEnumerator Co_CreateRoomAssets()
    {
        yield return new WaitForSeconds(0.5f);
        if (!thisRoom.isCleared)
        {
            spawnedRoom = Instantiate(startRoom, transform);
        }
    }

    public void DestroyStartRoom()
    {
        if (spawnedRoom == null) return;
        Destroy(spawnedRoom);
    }
}
