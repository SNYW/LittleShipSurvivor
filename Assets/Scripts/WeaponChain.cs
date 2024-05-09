 using System;
 using Object = UnityEngine.Object;

 public class WeaponChain
 {
     private readonly WeaponChainDefinition _def;

     private int _currentLevel = -1;
     private Weapon _activeWeapon;

     public WeaponChain(WeaponChainDefinition def)
     {
         _def = def;
     }

     public void Upgrade()
     {
         try
         {
             Object.Destroy(_activeWeapon.gameObject);
         }
         catch (Exception e)
         {
             // ignored
         }

         _currentLevel++;
         _activeWeapon = WeaponManager.AddWeapon(_def.levels[_currentLevel]);
     }

     public bool TryGetNextLevel(out WeaponDefinition weapon)
     {
         weapon = null;

         if (_currentLevel == -1)
         {
             weapon = _def.levels[0];
             return true;
         }
         
         if (_def.levels.Count <= _currentLevel + 1) { return false; }

         weapon = _def.levels[_currentLevel + 1];
         return true;
     }

     public WeaponChainDefinition GetDefinition()
     {
         return _def;
     }

     public int GetCurrentLevel()
     {
         return _currentLevel;
     }

     public void Reset()
     {
         _currentLevel = 0;
     }
 }
