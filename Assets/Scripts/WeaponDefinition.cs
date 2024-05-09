using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Definition", menuName = "Game Data/Weapon Definition")]
public class WeaponDefinition : ScriptableObject
{
    public string id;
    public Weapon weaponPrefab;
    [TextArea(20, 100)]
    public string description;
}
