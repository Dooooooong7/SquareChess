using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public ObjectEventSO enterRoomEventSO;
    public int roomNum;
    
    private void Awake()
    {
        roomNum = GetComponentInParent<Room>().serialNum;
    }
    private void OnMouseDown()
    {
        enterRoomEventSO.RaiseEvent(roomNum,this);
    }
}
