using SystemEvents;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseOption : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text chainName;
    public TMP_Text upgradeDescription;
    public TMP_Text upgradeLevel;
    public Image upgradeIcon;

    private WeaponChain _chain;
    
    public void Init(WeaponChain chain)
    {
        _chain = chain;
        var definition = chain.GetDefinition();
        if (!chain.TryGetNextLevel(out var nextWeapon))
        {
            Debug.LogError($"Trying to present an impossible option");
        }
        chainName.text = definition.chainName;
        upgradeDescription.text = nextWeapon.description;
        upgradeLevel.text = $"Lvl {chain.GetCurrentLevel()+2}";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        WeaponManager.UpgradeChain(_chain.GetDefinition());
        SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.UpgradeChosen, _chain);
    }
}
