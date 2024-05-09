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
                if (Vector2.Distance(pos, gameObject.transform.position) <
                    Vector2.Distance(pos, returnObj.transform.position))
                {
                    returnObj = gameObject;
                }
            }

            return returnObj;
        }
    }
}
