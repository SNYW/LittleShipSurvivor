using System.Collections;
using MovementBehaviours;
using ObjectPooling;
using Spells.SpellEffects;
using UnityEngine;

public class Unit : PooledObject
{
   public int maxHealth;
   private int _currentHealth;
   public float moveSpeed;
   public MovementBehaviour movementBehaviour;
   public SpellEffect[] onDeathEffects;
   private Vector3 _targetMoveDirection;
   protected Rigidbody2D Rb;

   private void Awake()
   {
      Rb = GetComponent<Rigidbody2D>();
   }

   private void OnEnable()
   {
      _currentHealth = maxHealth;
      StartCoroutine(ManageMove());
   }

   public virtual void OnHit(Transform tr, int amt)
   {
      _currentHealth -= amt;
      if (_currentHealth <= 0)
         Die();
   }

   private void Update()
   {
      if(movementBehaviour != null && !movementBehaviour.isPhysicsMove)
         transform.Translate(_targetMoveDirection.normalized * (moveSpeed * Time.deltaTime));
   }

   private void FixedUpdate()
   {
      if(movementBehaviour != null && movementBehaviour.isPhysicsMove && Rb != null)
         Rb.AddForce(_targetMoveDirection.normalized * moveSpeed, ForceMode2D.Force);
   }

   public IEnumerator ManageMove()
   {
      if (movementBehaviour == null) yield break;
      
      while (gameObject.activeSelf)
      {
         _targetMoveDirection = movementBehaviour.GetTargetDirection(transform);
         
         if(movementBehaviour.updateCooldown == -1)
            break;
         
         yield return new WaitForSeconds(movementBehaviour.updateCooldown);
      }
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
