using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public static class WeaponManager
{
    public static Dictionary<WeaponChainDefinition, WeaponChain> chains = new();
    private static Transform _weaponAnchor;

    public static void Init()
    {
        chains.Clear();
        
        Resources.LoadAll("Data/Weapon Chains", typeof(WeaponChainDefinition))
            .Cast<WeaponChainDefinition>().ToList()
            .ForEach(InitChain);
        
        _weaponAnchor = GameObject.Find("Weapon Anchor").transform;
    }

    private static void InitChain(WeaponChainDefinition def)
    {
        if (def.levels.Count == 0)
        {
            Debug.LogError($"Weapon Chain {def.name}::{def.chainName} has no levels");
        }
        else
        {
            chains.Add(def,new WeaponChain(def));
        }
    }
    
    public static Weapon AddWeapon(WeaponDefinition def)
    {
        var weapon = Object.Instantiate(def.weaponPrefab, _weaponAnchor);
        
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localPosition = Vector3.zero;
        weapon.GetComponent<Weapon>().Activate();
        return weapon;
    }

    public static void UpgradeChain(WeaponChainDefinition chain)
    {
       chains[chain].Upgrade();
    }

    public static List<WeaponChain> GetPossibleWeaponChains()
    {
        List<WeaponChain> possibleChains = new();
        
        foreach (var chain in chains)
        {
            if(chain.Value.TryGetNextLevel(out var weapon))
                possibleChains.Add(chain.Value);
        }
        
        return possibleChains;
    }

    public static List<WeaponChain> GetLevelUpOptions(int amount)
    {
        var possible = GetPossibleWeaponChains();

        var returnAmount = amount > possible.Count ? possible.Count : amount;
        possible.Shuffle();

        return possible.GetRange(0, returnAmount);
    }
    
    public static void ResetChains()
    {
        foreach (var chain in chains)
        {
            chain.Value.Reset();
        }
    }
}
