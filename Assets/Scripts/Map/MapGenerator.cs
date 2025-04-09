using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : Singleton<MapGenerator>
{
    [Header("地图配置")]
    public Room roomPrefab;
    
    [FormerlySerializedAs("mapDataSo")] [FormerlySerializedAs("roomDataSO")] [Header("房间数据SO")]
    public MapDataSO mapDataSO; // 存储所有房间数据
    private List<Room> roomList = new();
    public int totalLevel;
    public int currentLevel;
    public float difficultyMultiplier;
    public LevelProgressDataSO levelProgressDataSO;
    public ObjectEventSO loadStartEventSO;
    
    private void OnEnable()
    {
        if (mapDataSO == null)
        {
            Debug.LogError("MapDataSO 未赋值！");
            return;
        }

        if (mapDataSO.roomSaveDataList == null || mapDataSO.roomSaveDataList.Count == 0)
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

    public void GenerateNewMap()
    {
        roomList.Clear();
        mapDataSO.roomSaveDataList = new List<RoomSaveData>();

        for (int i = 0; i < 3; i++)
        {
            RoomType type = (i < 2) ? RoomType.Normal : RoomType.Boss;
            RoomSaveData saveData = new RoomSaveData
            {
                serialNum = i,
                roomType = type,
                roomState = i == 0? RoomState.Attainable : RoomState.Locked,
                roundNum = 3,
                enemyPerRound = new List<int> { 2, 1, 0 }, 
                heroPerRound = new List<int> { 3, 2, 1 } 
            };
            mapDataSO.roomSaveDataList.Add(saveData);// 添加到保存数据列表

            // 实例化房间
            Room newRoom = Instantiate(roomPrefab, transform);
            newRoom.SetupRoom(saveData);

            // 简单排一下位置
            newRoom.transform.localPosition = new Vector3(i * 3.0f - 3.0f, 0, 0);

            roomList.Add(newRoom);
        }
    }
    
    public void ClearMap()
    {
        foreach (var room in roomList)
        {
            Destroy(room.gameObject);
        }
        roomList.Clear();
        mapDataSO.roomSaveDataList.Clear();
    }

    private void SaveRoomData()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(mapDataSO);
        UnityEditor.AssetDatabase.SaveAssets();
        Debug.Log("MapDataSO 保存成功！");
#endif
    }

    private void LoadMap()
    {
        if (mapDataSO.roomSaveDataList[2].roomState == RoomState.Visited)
        {
            Debug.Log("该层已通过，生成新地图");
            ClearMap();
            if (++levelProgressDataSO.currentLevel <= levelProgressDataSO.totalLevels)
            {
                GenerateNewMap();
                return;
            }
            else
            {
                Debug.Log("游戏结束，返回开始界面");
                loadStartEventSO.RaiseEvent(null,this);
                return;
            }
        }
        foreach (var saveData in mapDataSO.roomSaveDataList)
        {
            Room newRoom = Instantiate(roomPrefab, transform);
            newRoom.SetupRoom(saveData);

            // 简单排一下位置
            newRoom.transform.localPosition = new Vector3(saveData.serialNum * 3.0f - 3.0f, 0, 0);

            roomList.Add(newRoom);
        }
    }

    //生成新的游戏进度
    public void SetLevelData(object value = null)
    {
        levelProgressDataSO.currentLevel = 1;
        levelProgressDataSO.totalLevels = 3;
        levelProgressDataSO.difficultyMultiplier = 1.0f;
        GenerateNewMap();
    }
    
}
