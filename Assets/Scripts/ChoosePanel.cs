using System.Collections.Generic;
using UnityEngine;

public class ChoosePanel : MonoBehaviour
{
    public ChooseOption optionPrefab;
    public Transform optionsAnchor;

    private void Start()
    {
        Init(WeaponManager.GetLevelUpOptions(3));
    }

    private void Init(List<WeaponChain> chains)
    {
        foreach (var chain in chains)
        {
            Instantiate(optionPrefab, optionsAnchor).Init(chain);
        }
    }
}
