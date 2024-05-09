using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Definition", menuName = "Game Data/Weapon Definition")]
public class WeaponDefinition : ScriptableObject
{
    public string id;
    public Weapon weaponPrefab;
    public string description;
}
