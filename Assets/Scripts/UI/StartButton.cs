using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button myButton;

    private void Start()
    {
        myButton = GetComponent<Button>();
    }
    
    public IEnumerator StartButtonClicked()
    {
        bool buttonClicked = false;

        // 定义一个临时函数来处理按钮点击事件
        myButton.onClick.AddListener(() => buttonClicked = true);

        // 等待直到按钮被点击
        while (!buttonClicked)
        {
            yield return null;
        }

        // 移除监听器，以避免重复添加
        myButton.onClick.RemoveAllListeners();
    }
}
