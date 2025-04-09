using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake() //懒汉式
    {
        _instance = this as T;
    }
}

