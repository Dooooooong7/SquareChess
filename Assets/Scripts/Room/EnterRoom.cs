using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public ObjectEventSO enterRoomEventSO;
    public RoomSaveData roomSaveData;
    
    private void OnMouseDown()
    {
        roomSaveData = GetComponentInParent<Room>().roomSaveData;
        if (roomSaveData.roomState == RoomState.Attainable)
        {
            enterRoomEventSO.RaiseEvent(roomSaveData,this);
            roomSaveData.roomState = RoomState.Visited;
            if (roomSaveData.serialNum < 2)
            {
                MapGenerator.Instance.mapDataSO.roomSaveDataList[roomSaveData.serialNum + 1].roomState = RoomState.Attainable;
            }
        }
        
    }
}
