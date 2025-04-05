using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapDataSO", menuName = "Map/MapDataSO")]
public class MapDataSO : ScriptableObject
{
    public List<RoomSaveData> roomSaveDataList = new();
}
[System.Serializable]
public class RoomSaveData
{
    public int serialNum;
    public RoomState roomState;
    public RoomType roomType;
    public int roundNum;
    public List<int> enemyPerRound;
}