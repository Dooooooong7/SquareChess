using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ObjectEventSO", menuName = "Events/ObjectEventSO")]
public class ObjectEventSO : BaseEventSO<object>
{
    public void OnButtonClick()
    {
        RaiseEvent(null, this);
    }

}
