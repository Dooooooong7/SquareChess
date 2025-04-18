using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class BuffCard : MonoBehaviour
{
    public Buff buff;
    public TMP_Text buffDescription;
    public Image buffIcon;

    private void Awake()
    {
        buffIcon = transform.Find("BuffIcon").GetComponent<Image>();
        Debug.Log(buffIcon);
        buffDescription = transform.Find("BuffInfo").GetComponent<TMP_Text>();
        Debug.Log(buffDescription);
    }

    public void Initialize(Buff buff)
    {
        this.buff = buff;
        buffIcon.sprite = buff.icon;
        buffDescription.text = buff.description;
    }
    
    public void AddBuffToHero()
    {
        foreach (var hero in HandManager.Instance.heroPoolSO.heroList)
        {
            if (buff.attainableCharacter.Contains(hero.hero))
            {
                // hero.hero.buffHandler.AddBuff(buff);
                hero.hero.GetComponent<BuffHandler>().AddBuff(buff);
                Debug.Log(" 添加Buff: " + buff.buffName + " 到角色: " + hero.hero.characterName);
            }
        }
    }

    public void OnButtonClick()
    {
        Debug.Log("点击了Buff卡片: " + buff.buffName);
    }
}
