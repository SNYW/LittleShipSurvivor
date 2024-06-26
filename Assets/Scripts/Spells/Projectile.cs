using System;
using System.Collections;
using ObjectPooling;
using Spells.SpellEffects;
using UnityEngine;
using UnityEngine.VFX;

namespace Spells
{
  public class Projectile : PooledObject
  {
    public float speed;
    public float lifetime;
    public float onCastDelay;

    [Range(1,60)]
    public float effectUpdateRate;
  
    public VisualEffect[] worldPosEffects;
  
    [SerializeReference]
    public SpellEffect[] onCastEffects;
  
    [SerializeReference]
    public SpellEffect[] onHitEffects;

    public virtual void InitProjectile()
    {
      StopAllCoroutines();
      StartCoroutine(TriggerAllEffects(onCastEffects, onCastDelay));
      StartCoroutine(UpdateVisualEffects(effectUpdateRate));
      foreach (var effect in worldPosEffects)
      {
        if(effect.HasVector3("startPos")) 
          effect.SetVector3("startPos", transform.position);
      }

      StartCoroutine(Deactivate());
    }

    private IEnumerator UpdateVisualEffects(float updateRate)
    {
      while (gameObject.activeSelf)
      {
        foreach (var effect in worldPosEffects)
        {
          if(effect.HasVector3("worldPos")) 
            effect.SetVector3("worldPos", transform.position);
        }

        yield return new WaitForSeconds(1/updateRate);
      }
    }

    private IEnumerator TriggerAllEffects(SpellEffect[] effects, float delay)
    {
      yield return new WaitForSeconds(delay);
      foreach (var effect in effects) 
      { 
        effect.Trigger(gameObject);
      }
    }
  
    private void TriggerAllEffectsImmediate(SpellEffect[] effects)
    {
      foreach (var effect in effects) 
      { 
        effect.Trigger(gameObject);
      }
    }

    protected virtual void Update()
    {
      transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    private IEnumerator Deactivate()
    {
      yield return new WaitForSeconds(lifetime);
      Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      Die();
    }

    private void Die()
    {
      StopAllCoroutines();
      TriggerAllEffectsImmediate(onHitEffects);
      StopAllCoroutines();
      ReQueue();
    }
  }
}
