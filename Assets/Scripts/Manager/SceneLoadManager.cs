using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public AssetReference map;
    public AssetReference battleField;
    private AssetReference currentScene;
    public AssetReference start;
    
    public async void OnLoadRoomEvent(object data)
    {
        currentScene = battleField;
        await UnloadSceneTask();
        //加载房间
        await LoadSceneTask();
        // 加载完成后，设置场景中的 BattleManager
        if (data is RoomSaveData roomSaveData)
        {
            // 查找场景中的 BattleManager 并设置其 RoomSaveData
            BattleManager battleManager = FindAnyObjectByType<BattleManager>();
            if (battleManager != null)
            {
                battleManager.SetRoomData(roomSaveData);
                // Debug.Log(roomSaveData.roundNum);
                Debug.Log("RoomSaveData 已传递给 BattleManager");
            }
            else
            {
                Debug.LogError("BattleManager 没有找到！");
            }
        }
    }

    /// <summary>
    /// 异步操作加载场景
    /// </summary>
    private async Awaitable LoadSceneTask()
    {
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }
    
    //卸载激活的场景
    private async Awaitable UnloadSceneTask()
    {
        // 获取当前加载的所有场景
        int sceneCount = SceneManager.sceneCount;
    
        // 只有当加载了多个场景时才尝试卸载
        if (sceneCount > 1)
        {
            Debug.Log("卸载场景");
            Debug.Log(SceneManager.GetActiveScene().name);
            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
        else
        {
            Debug.Log("只有一个场景加载（持久场景），无需卸载");
        }
        
    }

    /// <summary>
    /// 监听返回房间事件
    /// </summary>
    public async void LoadMap()
    {
        await UnloadSceneTask();
        currentScene = map;
        await LoadSceneTask();
    }
    
    public async void LoadStartScene()
    {
        currentScene = start;
        await LoadSceneTask();
        await SceneManager.UnloadSceneAsync("Map");
    }
}