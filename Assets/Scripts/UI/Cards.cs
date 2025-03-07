using UnityEngine;
using UnityEngine.UI;
using Utility;

public class Cards : MonoBehaviour
{
    public HeroType heroType;
    public Image selectedImage;
    public Image heroIdle;
    public bool isSelected = false;
    
    private void Update()
    {
        if (isSelected)
        {
            selectedImage.enabled = true;
        }
        else
        {
            selectedImage.enabled = false;
        }
    }

    public void OnClick()
    {
        isSelected = !isSelected;
            HandManager.Instance.heroType = heroType;
            HandManager.Instance.nowCard = this;
            HandManager.Instance.hasHero = isSelected;
    }
    
}
