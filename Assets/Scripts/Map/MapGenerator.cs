using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    [Header("地图配置")]
    public Room roomPrefab;
    
    [FormerlySerializedAs("roomDataSO")] [Header("房间数据SO")]
    public MapDataSO mapDataSo; // 存储所有房间数据


    private List<Room> roomList = new();

    private void OnEnable()
    {
        if (mapDataSo == null)
        {
            Debug.LogError("MapDataSO 未赋值！");
            return;
        }

        if (mapDataSo.roomSaveDataList == null || mapDataSo.roomSaveDataList.Count == 0)
        {
            Debug.Log("RoomDataSO为空，生成新地图");
            GenerateNewMap();
            SaveRoomData();
        }
        else
        {
            Debug.Log("RoomDataSO已有数据，读取生成地图");
            LoadMap();
        }
    }

    private void GenerateNewMap()
    {
        roomList.Clear();
        mapDataSo.roomSaveDataList = new List<RoomSaveData>();

        for (int i = 0; i < 3; i++)
        {
            RoomType type = (i < 2) ? RoomType.Normal : RoomType.Boss;
            RoomSaveData saveData = new RoomSaveData
            {
                serialNum = i,
                roomType = type,
                roomState = RoomState.Locked,
                roundNum = 0,
                enemyPerRound = new List<int> { 2, 3, 4 } // 随便给一个初始的敌人数量列表
            };
            mapDataSo.roomSaveDataList.Add(saveData);// 添加到保存数据列表

            // 实例化房间
            Room newRoom = Instantiate(roomPrefab, transform);
            newRoom.SetupRoom(saveData);

            // 简单排一下位置
            newRoom.transform.localPosition = new Vector3(i * 3.0f - 3.0f, 0, 0);

            roomList.Add(newRoom);
        }
    }

    private void SaveRoomData()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(mapDataSo);
        UnityEditor.AssetDatabase.SaveAssets();
        Debug.Log("MapDataSO 保存成功！");
#endif
    }

    private void LoadMap()
    {
        foreach (var saveData in mapDataSo.roomSaveDataList)
        {
            Room newRoom = Instantiate(roomPrefab, transform);
            newRoom.SetupRoom(saveData);

            // 简单排一下位置
            newRoom.transform.localPosition = new Vector3(saveData.serialNum * 3.0f - 3.0f, 0, 0);

            roomList.Add(newRoom);
        }
    }
}
