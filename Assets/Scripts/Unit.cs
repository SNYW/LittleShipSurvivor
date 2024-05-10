using System;
using ObjectPooling;
using Spells.SpellEffects;
using UnityEngine;

public class Unit : PooledObject
{
   public int maxHealth;
   private int _currentHealth; 
   public SpellEffect[] onDeathEffects;

   private void OnEnable()
   {
      _currentHealth = maxHealth;
   }

   public virtual void OnHit(Transform tr, int amt)
   {
      _currentHealth -= amt;
      if(_currentHealth <= 0)
         Die();
   }

   protected void Die()
   {
      foreach (var effect in onDeathEffects)
      {
         effect.Trigger(gameObject);
      }
      ReQueue();
   }
}
