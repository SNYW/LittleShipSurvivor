using UnityEngine;
using UnityEngine.EventSystems;
public class ThrustButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float rotationAmount;
    public bool held;

    private void Update()
    {
        switch (rotationAmount)
        {
            case < 0:
                GameManager.turningLeft = held;
                break;
            case > 0:
                GameManager.turningRight = held;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        held = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        held = false;
    }
}
