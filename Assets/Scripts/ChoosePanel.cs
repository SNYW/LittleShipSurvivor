using System.Collections.Generic;
using SystemEvents;
using UnityEngine;

public class ChoosePanel : MonoBehaviour
{
    public ChooseOption optionPrefab;
    public Transform optionsAnchor;

    private void Start()
    {
        var options = WeaponManager.GetLevelUpOptions(3);
        if(options.Count == 0) Destroy(gameObject);
        Init(options);
    }

    private void Init(List<WeaponChain> chains)
    {
        foreach (var chain in chains)
        {
            Instantiate(optionPrefab, optionsAnchor).Init(chain);
        }
    }
    
    private void OnEnable()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.UpgradeChosen, OnUpgradeChosen);
    }

    private void OnUpgradeChosen(object obj)
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.UpgradeChosen, OnUpgradeChosen);
    }
}
