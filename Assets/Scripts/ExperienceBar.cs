using SystemEvents;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Image fillImage;

    private void Start()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameStart, OnGameStart);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.ExperienceGained, OnExperienceGained);
    }

    private void OnGameStart(object obj)
    {
        fillImage.fillAmount = 0;
    }
    
    private void OnExperienceGained(object obj)
    {
        fillImage.fillAmount = GameManager.instance.CurrentExpPercentage;
        Debug.Log(GameManager.instance.CurrentExpPercentage);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameStart, OnGameStart);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.ExperienceGained, OnExperienceGained);
    }
}
