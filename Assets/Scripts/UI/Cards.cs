using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public HeroType heroType;
    public Image selectedImage;
    public Image heroIdle;
    public bool isSelected = false;
    
    //当点击手牌时，设置HandManager的当前手牌和英雄类型，高亮手牌
    public void OnClick()
    {
        isSelected = !isSelected;
        HandManager.Instance.heroType = heroType;
        HandManager.Instance.nowCard = this;
        HandManager.Instance.hasHero = isSelected;
        if (isSelected)
        {
            selectedImage.enabled = true;
        }
        else
        {
            selectedImage.enabled = false;
        }
    }
    
}