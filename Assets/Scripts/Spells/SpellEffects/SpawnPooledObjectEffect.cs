using ObjectPooling;
using UnityEngine;

namespace Spells.SpellEffects
{
    [CreateAssetMenu(menuName = "Game Data/Spell Effects/Spawn Pooled Object Effect")]
    public class SpawnPooledObjectEffect : SpellEffect
    {
        public int amount = 1;
        public ObjectPool pool;
        public float spawnRadius;
        
        public override void Trigger(GameObject obj)
        {
            for (int i = 0; i < amount; i++)
            {
                SpawnTargetObject(obj);
            }
        }

        private void SpawnTargetObject(GameObject gameObject)
        {
            var pooledObj = pool.GetPooledObject();
            var pos = GameManager.GetRandomPointInRadius(gameObject.transform.position, spawnRadius);
            pooledObj.transform.position = pos;
            pooledObj.SetActive(true);
        }
    }
}
