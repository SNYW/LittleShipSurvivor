using UnityEngine;

namespace MovementBehaviours
{
    public abstract class MovementBehaviour : ScriptableObject
    {
        public bool isPhysicsMove; 
        public float updateCooldown;
        public abstract Vector3 GetTargetDirection(Transform t);
    }
}
