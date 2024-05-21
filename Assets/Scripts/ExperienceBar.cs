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
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.LevelUp, OnExperienceGained);
    }

    private void OnGameStart(object obj)
    {
        fillImage.fillAmount = 0;
    }
    
    private void OnExperienceGained(object obj)
    {
        LeanTween.cancelAll(gameObject);
        LeanTween.value(gameObject, fillImage.fillAmount, GameManager.instance.CurrentExpPercentage, 0.1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnUpdate(
            val =>
            {
                fillImage.fillAmount = val;
            });
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameStart, OnGameStart);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.ExperienceGained, OnExperienceGained);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.LevelUp, OnExperienceGained);
    }
}
