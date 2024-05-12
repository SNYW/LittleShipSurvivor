using UnityEngine;

namespace Utilities
{
    public static class Vector2Extensions
    {
        public static float GetRandomInVector(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        public static Collider2D GetClosestCollider2D(this Vector2 pos, Collider2D[] objects)
        {
            var returnObj = objects[0];
            
            foreach (var gameObject in objects)
            {
                if(gameObject== null) continue;
                
                if (Vector2.Distance(pos, gameObject.transform.position) <
                    Vector2.Distance(pos, returnObj.transform.position))
                {
                    returnObj = gameObject;
                }
            }

            return returnObj;
        }

        public static float FastDistance(this Vector2 pos, Vector2 otherPos)
        {
            float squaredDistance = (pos - otherPos).sqrMagnitude;
            
            return Mathf.Sqrt(squaredDistance);
        }
    }
}
