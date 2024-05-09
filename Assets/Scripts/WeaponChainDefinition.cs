using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Chain Definition", menuName = "Game Data/Weapon Chain Definition")]
public class WeaponChainDefinition : ScriptableObject
{
    public string chainName;
    public List<WeaponDefinition> levels;

}
