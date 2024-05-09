using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool projectilePool;
    public float cooldown;

    public List<Transform> projectileAnchors;
    
    public void Activate()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (gameObject.activeSelf)
        {
            foreach (var anchor in projectileAnchors)
            {
                SpawnProjectile(anchor);
            }
            
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnProjectile(Transform t)
    {
        var projectile = projectilePool.GetPooledObject();
        projectile.transform.position = t.position;
        projectile.transform.up = t.up;
        projectile.SetActive(true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}