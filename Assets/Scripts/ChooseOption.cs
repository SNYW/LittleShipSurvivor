using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseOption : MonoBehaviour
{
    public TMP_Text chainName;
    public TMP_Text upgradeDescription;
    public TMP_Text upgradeLevel;
    public Image upgradeIcon;
    
    public void Init(WeaponChain chain)
    {
        var definition = chain.GetDefinition();
        if (!chain.TryGetNextLevel(out var nextWeapon))
        {
            Debug.LogError($"Trying to present an impossible option");
        }
        chainName.text = definition.chainName;
        upgradeDescription.text = nextWeapon.description;
        upgradeLevel.text = $"Lvl {chain.GetCurrentLevel()+1}";
    }
}
