using System;
using System.Collections;
using MovementBehaviours;
using Spells;
using UnityEngine;

public class HomingProjectile : Projectile
{
   public float turnSpeed;
   public MovementBehaviour movementBehaviour;

   private Vector3 _targetMoveDirection;
   private Rigidbody2D _rb;

   public override void InitProjectile()
   {
      _rb = GetComponent<Rigidbody2D>();
      StartCoroutine(ManageMove());
      base.InitProjectile();
   }

   protected override void Update()
   {
      var direction = _targetMoveDirection.normalized;

      Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: direction);

      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
      _rb.AddForce(direction*speed*Time.deltaTime, ForceMode2D.Force);
   }

   private IEnumerator ManageMove()
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
}
