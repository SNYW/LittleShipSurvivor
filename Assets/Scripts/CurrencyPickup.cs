using System.Collections;
using ObjectPooling;
using UnityEngine;
using Utilities;

public class CurrencyPickup : PooledObject
{
   public float moveSpeed;
   public int amount;

   private bool _active;
   private Collider2D _col;

   private void Awake()
   { 
      _col = GetComponent<Collider2D>();
   }

   private void OnEnable()
   {
      _active = true;
     
      _col.enabled = true;
   }

   public void OnPickup()
   {
      if (!_active) return;
      
      _col.enabled = false;
      StartCoroutine(PickUp());
   }

   private IEnumerator PickUp()
   {
      while (((Vector2)transform.position).FastDistance(GameManager.playerShip.transform.position) > 1f)
      {
         var direction = GameManager.playerShip.transform.position - transform.position;
         transform.Translate(direction.normalized*moveSpeed*Time.deltaTime);
         yield return new WaitForEndOfFrame();
      }
      
      GameManager.instance.AddExperience(amount);
      ReQueue();
   }
}
