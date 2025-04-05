using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType roomType;
    public int serialNum;
    public RoomState roomState;
    public SpriteRenderer spriteRenderer;
    public RoomSaveData roomSaveData;
    public int roundNum;
    public List<int> enemyPerRound; 
    
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void SetupRoom(RoomSaveData roomSaveData)
    {
        // 直接通过RoomSaveData设置房间的状态和数据
        this.roomSaveData = roomSaveData;
        this.serialNum = roomSaveData.serialNum;
        this.roundNum = roomSaveData.roundNum;
        this.enemyPerRound = new List<int>(roomSaveData.enemyPerRound); // 根据RoomSaveData初始化敌人信息
    
        // 设置房间的颜色和状态
        roomState = roomSaveData.roomState;
        spriteRenderer.color = roomState switch
        {
            RoomState.Locked => Color.white,
            RoomState.Visited => new Color(0.5f, 0.5f, 0.5f, 0.5f),
            RoomState.Attainable => Color.white,
            _ => spriteRenderer.color
        };
    }

    
    
}
