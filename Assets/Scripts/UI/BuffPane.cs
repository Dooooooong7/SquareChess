using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPane : MonoBehaviour
{
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;
    public float animationSpeed;
    public GameObject panel;

    private IEnumerator ShowPanel(GameObject gameObject)
    {
        BuffManager.Instance.GenerateBuffCard();
        float timer = 0;
        while (timer < 1)
        {
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }
    
    private IEnumerator HidePanel(GameObject gameObject)
    {
        float timer = 0;
        while (timer < 1)
        {
            gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
        }
    }

    public void ShowBuffPanel()
    {
        StartCoroutine(ShowPanel(panel));
    }
    
    public void HideBuffPanel()
    {
        StartCoroutine(HidePanel(panel));
    }
    
}
