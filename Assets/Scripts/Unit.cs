using System.Collections;
using MovementBehaviours;
using ObjectPooling;
using Spells.SpellEffects;
using UnityEngine;
using Utilities;

public class Unit : PooledObject
{
   public int maxHealth;
   private int _currentHealth;
   public float moveSpeed;
   public MovementBehaviour movementBehaviour;
   public SpellEffect[] onDeathEffects;
   private Vector3 _targetMoveDirection;
   protected Rigidbody2D Rb;
   private PlayerUnit _playerShip => GameManager.playerShip;

   private void Awake()
   {
      Rb = GetComponent<Rigidbody2D>();
   }

   private void OnEnable()
   {
      _currentHealth = maxHealth;
   }

   public override void Activate()
   {
      StartCoroutine(ManageMove());
      StartCoroutine(ResetMove());
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
      if(movementBehaviour != null && movementBehaviour.isPhysicsMove && Rb != null && movementBehaviour.updateCooldown != -1)
         Rb.AddForce(_targetMoveDirection.normalized * moveSpeed, ForceMode2D.Force);
   }

   public IEnumerator ManageMove()
   {
      if (movementBehaviour == null) yield break;
      
      while (gameObject.activeSelf)
      {
         _targetMoveDirection = movementBehaviour.GetTargetDirection(transform);
         
         if(movementBehaviour.isPhysicsMove && movementBehaviour.updateCooldown == -1)
            Rb.AddForce(_targetMoveDirection.normalized * moveSpeed, ForceMode2D.Force);
         
         if(movementBehaviour.updateCooldown == -1)
            break;
         
         yield return new WaitForSeconds(movementBehaviour.updateCooldown);
      }
   }

   public IEnumerator ResetMove()
   {
      while (gameObject.activeSelf)
      {
         if (_playerShip == null)
         {
            yield return new WaitForSeconds(2);
            continue;
         }
         
         if (((Vector2)transform.position).FastDistance(_playerShip.transform.position) >= 150)
         {
            yield return new WaitForSeconds(Random.Range(0, 4));
            
            Rb.velocity = Vector2.zero;

            var randomOffset = new Vector3(Random.Range(10, 20), Random.Range(10, 20), 0);
            transform.position = _playerShip.transform.position + _playerShip.transform.up * 150 + randomOffset;
            
            _targetMoveDirection = movementBehaviour.GetTargetDirection(transform);
            
            if(movementBehaviour.isPhysicsMove)
               Rb.AddForce(_targetMoveDirection.normalized * moveSpeed, ForceMode2D.Force);
         }
         
         yield return new WaitForSeconds(2);
      }
   }

   protected void Die()
   {
      foreach (var effect in onDeathEffects)
      {
         effect.Trigger(gameObject);
      }
      StopAllCoroutines();
      ReQueue();
   }
}
