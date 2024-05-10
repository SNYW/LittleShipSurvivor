using System;
using UnityEngine;

[Serializable]
public class WeaponDefinition
{
    public Weapon weaponPrefab;
    public Sprite weaponSprite;
    [TextArea(1, 3)]
    public string description;
}
