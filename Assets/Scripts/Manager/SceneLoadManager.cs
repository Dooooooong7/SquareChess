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
    [Header("加载房间后广播，将之后的房间设置为attainable")]
    public ObjectEventSO afterLoadRoomEvent;
    
    public async void OnLoadRoomEvent(object data)
    {
        currentScene = battleField;
        await UnloadSceneTask();
        //加载房间
        await LoadSceneTask();
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
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
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
}